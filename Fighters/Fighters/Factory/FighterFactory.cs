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

    public FighterFactory( IConsoleInputReader consoleReader )
    {
        _consoleReader = consoleReader;
    }

    public IFighter CreateFighter()
    {
        Console.Write( Messages.AskFighterNameMessage );
        string fighterName = _consoleReader.GetValidUserStringInput();

        IRace race = SelectAvailableOption(
            FighterOption.Races,
            Messages.SelectRaceMessage,
            Messages.RaceOptionsMessage );

        IWeapon weapon = SelectAvailableOption(
            FighterOption.Weapons,
            Messages.SelectWeaponMessage,
            Messages.WeaponOptionsMessage );

        IArmor armor = SelectAvailableOption(
            FighterOption.Armors,
            Messages.SelectArmorMessage,
            Messages.ArmorOptionsMessage );

        IFighterClass fighterClass = SelectAvailableOption(
            FighterOption.FighterClasses,
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
