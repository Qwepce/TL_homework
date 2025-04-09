using Dictionary.Enums;
using Dictionary.Implementations;
using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary;

public class Program
{
    private const string FilePath = "translations.txt";

    public static void Main()
    {
        ITranslatorService translatorService = new TranslatorService( FilePath );
        ICustomValidator validator = new CustomValidator();

        string word;
        string newWord;
        string newTranslation;

        Console.WriteLine( Messages.GreetingMessage );
        Console.WriteLine( Messages.MenuMessage );

        bool isExitSelected = false;

        while ( !isExitSelected )
        {
            Console.Write( Messages.AskUserChoiceMessage );
            int userCommandChoice = validator.GetValidUserChoice();

            switch ( ( UserCommand )userCommandChoice )
            {
                case UserCommand.RussianToEnglish:
                    word = validator.GetValidUserInput( Messages.AskUserInputMessage );

                    translatorService.GetTranslation( word, isEnglishToRussian: false );
                    break;

                case UserCommand.EnglishToRussian:
                    word = validator.GetValidUserInput( Messages.AskUserInputMessage );

                    translatorService.GetTranslation( word, isEnglishToRussian: true );
                    break;

                case UserCommand.AddNewRussianWord:
                    newWord = validator.GetValidUserInput( Messages.AskUserNewRussianWord );
                    newTranslation = validator.GetValidUserInput( Messages.AskUserNewTranslation );

                    translatorService.AddNewTranslation( newWord, newTranslation );
                    break;

                case UserCommand.AddNewEnglishWord:
                    newTranslation = validator.GetValidUserInput( Messages.AskUserNewEnglishWord );
                    newWord = validator.GetValidUserInput( Messages.AskUserNewTranslation );

                    translatorService.AddNewTranslation( newWord, newTranslation );
                    break;

                case UserCommand.GetAllTranslations:
                    translatorService.GetAllTranslations();
                    break;

                case UserCommand.Exit:
                    isExitSelected = true;
                    break;

                default:
                    Console.WriteLine( Messages.UnknownSelectedCommand );
                    break;
            }
        }

        Console.WriteLine( Messages.FarewellMessage );
    }
}