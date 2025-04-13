using Fighters.Utils;

namespace Fighters.Validator;

public class CustomValidator : ICustomValidator
{
    public int GetPositiveIntegerInput( string errorMessage )
    {
        string userCommandInput = Console.ReadLine();

        int userCommandChoice;

        while ( !int.TryParse( userCommandInput, out userCommandChoice ) || userCommandChoice <= 0 )
        {
            Console.Write( Messages.InvalidUserNumberInput );

            userCommandInput = Console.ReadLine();
        }

        return userCommandChoice;
    }

    public string GetValidUserInput( string message )
    {
        Console.Write( message );
        string userInput;

        while ( string.IsNullOrEmpty( userInput = Console.ReadLine()?.Trim() ) )
        {
            Console.WriteLine( Messages.InvalidUserInput );
            Console.Write( message );
        }

        return userInput;
    }
}
