using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    private readonly string _name;
    private readonly IRace _race;
    private readonly IWeapon _weapon;
    private readonly IArmor _armor;
    private readonly IFighterClass _fighterClass;

    private int _currentHealth;
    private int _currentArmor;

    private const double MinDamageMultiplicator = 0.8;
    private const double MaxDamageMultiplicator = 1.1;
    private const double CriticalHitChance = 0.15;

    public Fighter(
        string name,
        IRace race,
        IWeapon weapon,
        IArmor armor,
        IFighterClass fighterClass )
    {
        _name = name;
        _race = race;
        _weapon = weapon;
        _armor = armor;
        _fighterClass = fighterClass;
        _currentHealth = GetMaxHealth();
        _currentArmor = GetMaxArmor();
    }

    public IRace GetRace() => _race;

    public IWeapon GetWeapon() => _weapon;

    public IArmor GetArmor() => _armor;

    public IFighterClass GetFighterClass() => _fighterClass;

    public string GetName() => _name;

    public int GetBaseDamage() => _race.Damage + _fighterClass.Damage + _weapon.Damage;

    public int GetMaxArmor() => _armor.Armor + _race.Armor;

    public int GetMaxHealth() => _race.Health + _fighterClass.Health;

    public int GetCurrentHealth() => _currentHealth;

    public int GetCurrentArmor() => _currentArmor;

    public int CalculateDamage()
    {
        double randomRoll = GetNextDouble();

        double randomDamageMultiplicator = MinDamageMultiplicator
            + ( randomRoll * ( MaxDamageMultiplicator - MinDamageMultiplicator ) );

        int totalDamage = ( int )( GetBaseDamage() * randomDamageMultiplicator );

        if ( randomRoll <= CriticalHitChance )
        {
            totalDamage = ( int )( totalDamage * 1.5 );
        }

        return totalDamage;
    }

    public void TakeDamage( int damage )
    {
        int armor = GetCurrentArmor();
        int damageToArmor = Math.Min( armor, damage );
        int damageToHealth = Math.Max( damage - armor, 0 );

        _currentArmor = Math.Max( armor - damageToArmor, 0 );

        if ( damageToHealth > 0 )
        {
            _currentHealth = Math.Max( _currentHealth - damageToHealth, 0 );
        }
    }

    internal virtual double GetNextDouble() => Random.Shared.NextDouble();
}
