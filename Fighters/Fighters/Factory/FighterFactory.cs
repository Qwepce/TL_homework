using Fighters.ConsoleReader;
using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utils;

namespace Fighters.Factory;

public class FighterFactory : IFighterFactory
{
    private readonly IConsoleInputReader _consoleReader;
    private readonly Dictionary<int, IRace> _races;
    private readonly Dictionary<int, IWeapon> _weapons;
    private readonly Dictionary<int, IArmor> _armors;
    private readonly Dictionary<int, IFighterClass> _fighterClasses;

    public FighterFactory(
        IConsoleInputReader consoleReader,
        Dictionary<int, IRace> races,
        Dictionary<int, IWeapon> weapons,
        Dictionary<int, IArmor> armors,
        Dictionary<int, IFighterClass> fighterClasses )
    {
        _consoleReader = consoleReader;
        _races = races;
        _weapons = weapons;
        _armors = armors;
        _fighterClasses = fighterClasses;
    }

    public IFighter CreateFighter()
    {
        Console.Write( Messages.AskFighterNameMessage );
        string fighterName = _consoleReader.GetValidUserStringInput();

        IRace race = SelectAvailableOption(
            _races,
            Messages.SelectRaceMessage,
            Messages.RaceOptionsMessage );

        IWeapon weapon = SelectAvailableOption(
            _weapons,
            Messages.SelectWeaponMessage,
            Messages.WeaponOptionsMessage );

        IArmor armor = SelectAvailableOption(
            _armors,
            Messages.SelectArmorMessage,
            Messages.ArmorOptionsMessage );

        IFighterClass fighterClass = SelectAvailableOption(
            _fighterClasses,
            Messages.SelectFighterClassMessage,
            Messages.FighterClassesOptionsMessage );

        IFighter createdFighter = new Fighter(
            fighterName,
            race,
            weapon,
            armor,
            fighterClass );

        return createdFighter;
    }

    private T SelectAvailableOption<T>(
        Dictionary<int, T> options,
        string askUserSelectOption,
        string optionsMenuMessage )
    {
        Console.WriteLine( askUserSelectOption );
        Console.WriteLine( optionsMenuMessage );

        Console.Write( Messages.AskUserSelectOption );

        int userSelectedOption = _consoleReader.GetValidPositiveIntegerInput();

        while ( !options.ContainsKey( userSelectedOption ) )
        {
            Console.Write( Messages.UnknownOptionSelected );
            userSelectedOption = _consoleReader.GetValidPositiveIntegerInput();
        }

        return options[ userSelectedOption ];
    }
}
