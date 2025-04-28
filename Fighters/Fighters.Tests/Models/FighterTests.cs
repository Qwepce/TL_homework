using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.Tests.Models;

public class FighterTests
{
    private readonly IFighter _fighter;
    private readonly Mock<IRace> _race;
    private readonly Mock<IWeapon> _weapon;
    private readonly Mock<IArmor> _armor;
    private readonly Mock<IFighterClass> _fighterClass;

    public FighterTests()
    {
        _race = new Mock<IRace>();
        _weapon = new Mock<IWeapon>();
        _armor = new Mock<IArmor>();
        _fighterClass = new Mock<IFighterClass>();

        _race.Setup( r => r.Health ).Returns( 100 );
        _race.Setup( r => r.Damage ).Returns( 10 );
        _race.Setup( r => r.Armor ).Returns( 5 );

        _weapon.Setup( w => w.Damage ).Returns( 25 );

        _armor.Setup( a => a.Armor ).Returns( 50 );

        _fighterClass.Setup( fc => fc.Health ).Returns( 50 );
        _fighterClass.Setup( fc => fc.Damage ).Returns( 10 );

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
        Assert.Equal( expected: 45, actual: fighterBaseDamage );
    }

    [Fact]
    public void GetMaxArmor_ShouldReturnCalculationArmorAndRaceArmor()
    {
        // Arrange

        // Act
        int fighterMaxArmor = _fighter.GetMaxArmor();

        // Assert
        Assert.Equal( expected: 55, actual: fighterMaxArmor );
    }

    [Fact]
    public void GetMaxHealth_ShoudReturnCalculationRaceAndFighterClassHealth()
    {
        // Arrange

        // Act
        int fighterMaxHealth = _fighter.GetMaxHealth();

        // Assert
        Assert.Equal( expected: 150, actual: fighterMaxHealth );
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
        double minDamage = 45 * 0.8;
        double maxDamage = 45 * 1.1;
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
        double minDamage = 45 * 0.8 * 1.5;
        double maxDamage = 45 * 1.1 * 1.5;
        Assert.InRange( actual: damage, ( int )minDamage, ( int )maxDamage );
    }

    [Fact]
    public void TakeDamage_DamageLessThanArmorAmount_ShouldDecreaseOnlyArmor()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( 10 );

        // Assert
        Assert.Equal( expected: 45, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 150, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageEqualToArmor_ShouldBeZeroArmorAndFullHealth()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( 55 );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 150, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageEqualToArmorAndHealth_ShouldBeZeroArmorAndZeroHealth()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( 205 );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreThanArmorAmount_ShouldDecreasedHealthAndArmor()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( 105 );

        // Assert
        Assert.Equal( expected: 55, actual: _fighter.GetMaxArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 150, actual: _fighter.GetMaxHealth() );
        Assert.Equal( expected: 100, actual: _fighter.GetCurrentHealth() );
    }

    [Fact]
    public void TakeDamage_DamageMoreThanArmorAndHealth_HealthAndArmorNeverLessThanZero()
    {
        // Arrange

        // Act
        _fighter.TakeDamage( 2_147_483_647 );

        // Assert
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentArmor() );
        Assert.Equal( expected: 0, actual: _fighter.GetCurrentHealth() );
    }
}
