using Dictionary.Enums;
using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary;

public class TranslatorManager
{
    public static void Run( ITranslatorService translatorService, ICustomValidator validator )
    {
        bool isExitCommandExecuted = false;

        while ( !isExitCommandExecuted )
        {
            Console.Write( Messages.AskUserChoiceMessage );
            int userCommandChoice = validator.GetValidUserChoice();

            isExitCommandExecuted = ExecuteCommand(
                ( UserCommand )userCommandChoice,
                translatorService,
                validator );
        }
    }

    private static bool ExecuteCommand(
        UserCommand command,
        ITranslatorService translatorService,
        ICustomValidator validator )
    {
        string word;
        string newWord;
        string newTranslation;

        switch ( command )
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
                return true;

            default:
                Console.WriteLine( Messages.UnknownSelectedCommand );
                break;
        }

        return false;
    }
}
