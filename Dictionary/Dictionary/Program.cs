using Dictionary.Enums;
using Dictionary.Implementations;
using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary;

public class Program
{
    private const string FILE_PATH = "translations.txt";

    public static void Main()
    {
        ITranslatorService translatorService = new TranslatorService( FILE_PATH );
        ICustomValidator validator = new CustomValidator();

        string word;
        string newWord;
        string newTranslation;

        Console.WriteLine( Messages.GREETING_MESSAGE );
        Console.WriteLine( Messages.MENU_MESSAGE );

        bool isExitSelected = false;

        while ( !isExitSelected )
        {
            Console.Write( Messages.ASK_USER_CHOICE_MESSAGE );
            int userCommandChoice = validator.GetValidUserChoice();

            switch ( ( UserCommands )userCommandChoice )
            {
                case UserCommands.RussianToEnglish:
                    word = validator.GetValidUserInput( Messages.ASK_USER_INPUT_MESSAGE );

                    translatorService.GetTranslation( word, isEnglishToRussian: false );
                    break;

                case UserCommands.EnglishToRussian:
                    word = validator.GetValidUserInput( Messages.ASK_USER_INPUT_MESSAGE );

                    translatorService.GetTranslation( word, isEnglishToRussian: true );
                    break;

                case UserCommands.AddNewRussianWord:
                    newWord = validator.GetValidUserInput( Messages.ASK_USER_NEW_RUSSIAN_WORD );
                    newTranslation = validator.GetValidUserInput( Messages.ASK_USER_NEW_TRANSLATION );

                    translatorService.AddNewTranslation( newWord, newTranslation );
                    break;

                case UserCommands.AddNewEnglishWord:
                    newTranslation = validator.GetValidUserInput( Messages.ASK_USER_NEW_ENGLISH_WORD );
                    newWord = validator.GetValidUserInput( Messages.ASK_USER_NEW_TRANSLATION );

                    translatorService.AddNewTranslation( newWord, newTranslation );
                    break;

                case UserCommands.GetAllTranslations:
                    translatorService.GetAllTranslations();
                    break;

                case UserCommands.Exit:
                    isExitSelected = true;
                    break;

                default:
                    Console.WriteLine( Messages.UNKNOWN_SELECTED_COMMAND_MESSAGE );
                    break;
            }
        }

        Console.WriteLine( Messages.FAREWELL_MESSAGE );
    }
}