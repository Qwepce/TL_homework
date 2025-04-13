using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Utils;
using Fighters.Validator;

namespace Fighters;

public static class FighterFactory
{
    private static ICustomValidator s_validator;

    public static IFighter CreateFighter( ICustomValidator validator )
    {
        SetValidator( validator );

        string fighterName = s_validator.GetValidUserInput( Messages.AskFighterNameMessage );

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

    private static T SelectAvailableOption<T>(
        Dictionary<int, T> options,
        string askUserSelectOption,
        string optionsMenuMessage )
    {
        Console.WriteLine( askUserSelectOption );
        Console.WriteLine( optionsMenuMessage );

        Console.Write( Messages.AskUserSelectOption );
        int userSelectedOption;

        do
        {
            userSelectedOption = s_validator.GetPositiveIntegerInput( Messages.UnknownOptionSelected );

            if ( !options.ContainsKey( userSelectedOption ) )
            {
                Console.Write( Messages.UnknownOptionSelected );
            }

        } while ( !options.ContainsKey( userSelectedOption ) );

        return options[ userSelectedOption ];
    }

    private static void SetValidator( ICustomValidator validator )
    {
        s_validator = validator;
    }
}
