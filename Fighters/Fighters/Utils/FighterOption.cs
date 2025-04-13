using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Utils;

public static class FighterOption
{
    public static readonly Dictionary<int, IRace> Races = new()
    {
        { 1, new Human() },
        { 2, new Goblin() },
        { 3, new Dwarf() },
    };

    public static readonly Dictionary<int, IWeapon> Weapons = new()
    {
        { 1, new NoWeapon() },
        { 2, new Dagger() },
        { 3, new Sword() },
        { 4, new Mace() },
        { 5, new Stick() },
        { 6, new Bow() },
    };

    public static readonly Dictionary<int, IArmor> Armors = new()
    {
        { 1, new NoArmor() },
        { 2, new ChainMail() },
        { 3, new Helmet() },
        { 4, new Shield() },
    };

    public static readonly Dictionary<int, IFighterClass> FighterClasses = new()
    {
        { 1, new Knight() },
        { 2, new Assassin() },
        { 3, new Archer() },
        { 4, new Wizard() }
    };
}
