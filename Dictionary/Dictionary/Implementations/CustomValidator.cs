using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary.Implementations;

public class CustomValidator : ICustomValidator
{
    public int GetValidUserChoice()
    {
        string userCommandInput = Console.ReadLine();

        int userCommandChoice;

        while ( !int.TryParse( userCommandInput, out userCommandChoice ) )
        {
            Console.Write( Messages.InvalidCommandInput );

            userCommandInput = Console.ReadLine();
        }

        return userCommandChoice;
    }

    public string GetValidUserInput( string askUserInput )
    {
        Console.Write( askUserInput );
        string userInput = Console.ReadLine();

        while ( string.IsNullOrEmpty( userInput ) )
        {
            Console.WriteLine( Messages.InvalidUserInput );
            Console.Write( askUserInput );

            userInput = Console.ReadLine();
        }

        return userInput.Trim().ToLower(); ;
    }
}
