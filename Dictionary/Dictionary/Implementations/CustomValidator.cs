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
        string userInput;

        while ( string.IsNullOrEmpty( userInput = Console.ReadLine()?.Trim().ToLower() ) )
        {
            Console.WriteLine( Messages.InvalidUserInput );
            Console.Write( askUserInput );
        }

        return userInput;
    }
}
