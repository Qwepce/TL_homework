using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.Tests.Models;

public class FighterTests
{
    private readonly Fighter _fighter;

    private readonly Mock<IRace> _race;
    private readonly Mock<IWeapon> _weapon;
    private readonly Mock<IArmor> _armor;
    private readonly Mock<IFighterClass> _fighterClass;

    private const int RaceHealth = 100;
    private const int RaceDamage = 10;
    private const int RaceArmor = 5;

    private const int WeaponDamage = 50;

    private const int ArmorValue = 50;
    private const int FighterClassHealth = 50;
    private const int FighterClassDamage = 10;

    private int MaxHealth => RaceHealth + FighterClassHealth;
    private int MaxArmor => ArmorValue + RaceArmor;
    private int BaseDamage => RaceDamage + WeaponDamage + FighterClassDamage;

    public FighterTests()
    {
        _race = new Mock<IRace>();
        _weapon = new Mock<IWeapon>();
        _armor = new Mock<IArmor>();
        _fighterClass = new Mock<IFighterClass>();

        _race.Setup( r => r.Health ).Returns( RaceHealth );
        _race.Setup( r => r.Damage ).Returns( RaceDamage );
        _race.Setup( r => r.Armor ).Returns( RaceArmor );

        _weapon.Setup( w => w.Damage ).Returns( WeaponDamage );

        _armor.Setup( a => a.Armor ).Returns( ArmorValue );

        _fighterClass.Setup( fc => fc.Health ).Returns( FighterClassHealth );
        _fighterClass.Setup( fc => fc.Damage ).Returns( FighterClassDamage );

        _fighter = new Fighter(
            "Боец 1",
            _race.Object,
            _weapon.Object,
            _armor.Object,
            _fighterClass.Object );
    }

    [Fact]
    public void GetBaseDamage_ShouldReturnCalculationRaceWeaponAndFighterClassDamage()
    {
        // Arrange

        // Act
        int fighterBaseDamage = _fighter.GetBaseDamage();

        // Assert
        Assert.Equal( expected: BaseDamage, actual: fighterBaseDamage );
    }

    [Fact]
    public void GetMaxArmor_ShouldReturnCalculationArmorAndRaceArmor()
    {
        // Arrange

        // Act
        int fighterMaxArmor = _fighter.GetMaxArmor();

        // Assert
        Assert.Equal( expected: MaxArmor, actual: fighterMaxArmor );
    }

    [Fact]
    public void GetMaxHealth_ShoudReturnCalculationRaceAndFighterClassHealth()
    {
        // Arrange

        // Act
        int fighterMaxHealth = _fighter.GetMaxHealth();

        // Assert
        Assert.Equal( expected: MaxHealth, actual: fighterMaxHealth );
    }

    [Fact]
    public void CalculateDamage_RandomRollMoreThanCriticalHitChance_ReturnsBaseDamageWithRandomDamageMultiplier()
    {
        // Arrange
        var fighterMock = new Mock<Fighter>(
            "Боец 1",
            _race.Object,
            _weapon.Object,
            _armor.Object,
            _fighterClass.Object )
        { CallBase = true };

        fighterMock
            .Setup( f => f.GetNextDouble() )
            .Returns( 0.5 );

        // Act
        int damage = fighterMock.Object.CalculateDamage();

        // Assert
        double minDamage = BaseDamage * 0.8;
        double maxDamage = BaseDamage * 1.1;
        Assert.InRange( damage, ( int )minDamage, ( int )maxDamage );
    }

    [Fact]
    public void CalculateDamage_RandomRollLessThanCriticalHitChance_ReturnsCriticalBaseDamageWithRandomDamageMultiplier()
    {
        // Arrange
        var fighterMock = new Mock<Fighter>(
            "Боец 1",
            _race.Object,
            _weapon.Object,
            _armor.Object,
            _fighterClass.Object )
        { CallBase = true };

        fighterMock
            .Setup( f => f.GetNextDouble() )
            .Returns( 0.1 );

        // Act
        int damage = fighterMock.Object.CalculateDamage();

        // Assert
        double minDamage = BaseDamage * 0.8 * 1.5;
        double maxDamage = BaseDamage * 1.1 * 1.5;
        Assert.InRange( actual: damage, ( int )minDamage, ( int )maxDamage );
    }

    [Fact]
    public void TakeDamage_DamageLessThanArmorAmount_ShouldDecreaseOnlyArmor()
    {
        // Arrange
        int damage = 10;
        int expectedRemainingArmor = MaxArmor - damage;
        int expectedRemainingHealth = MaxHealth;

        // Act
        _fighter.TakeDamage( damage );

        // Assert
        Assert.Equal( expected: expectedRemainingArmor, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: expectedRemainingHealth, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageEqualToArmor_ShouldBeZeroArmorAndFullHealth()
    {
        // Arrange
        int damage = MaxArmor;

        // Act
        _fighter.TakeDamage( damage );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: MaxHealth, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageEqualToArmorAndHealth_ShouldBeZeroArmorAndZeroHealth()
    {
        // Arrange
        int damage = MaxArmor + MaxHealth;

        // Act
        _fighter.TakeDamage( damage );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreThanArmorAmount_ShouldDecreasedHealthAndArmor()
    {
        // Arrange
        int damage = MaxArmor + 50;

        // Act
        _fighter.TakeDamage( damage );

        // Assert
        Assert.Equal( expected: MaxArmor, actual: _fighter.GetMaxArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: MaxHealth, actual: _fighter.GetMaxHealth() );
        Assert.Equal( expected: MaxHealth - 50, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreThanArmorAndHealth_HealthAndArmorNeverLessThanZero()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( int.MaxValue );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentHealth() );
    }
}
