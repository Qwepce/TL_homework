using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string GetName();

    int GetBaseDamage();

    int GetMaxArmor();

    int GetMaxHealth();

    int GetCurrentHealth();

    int GetCurrentArmor();

    int CalculateDamage();

    void TakeDamage( int damage );

    IRace GetRace();

    IWeapon GetWeapon();

    IArmor GetArmor();

    IFighterClass GetFighterClass();
}
