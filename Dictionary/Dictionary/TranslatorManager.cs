using Dictionary.Enums;
using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary;

public class TranslatorManager
{
    public static void Run( ITranslatorService translatorService, ICustomValidator validator )
    {
        while ( true )
        {
            Console.Write( Messages.AskUserChoiceMessage );
            UserCommand userCommandChoice = ( UserCommand )validator.GetValidUserChoice();

            if ( userCommandChoice.Equals( UserCommand.Exit ) )
            {
                break;
            }

            ProcessCommand(
                userCommandChoice,
                translatorService,
                validator );
        }
    }

    private static void ProcessCommand(
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

            default:
                Console.WriteLine( Messages.UnknownSelectedCommand );
                break;
        }
    }
}
