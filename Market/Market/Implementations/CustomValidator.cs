using Market.Interfaces;
using Market.Model;

namespace Market.Implementations;

public class CustomValidator : ICustomValidator
{
    public string GetValidInputFromConsole( string message, string errorMessage )
    {
        Console.Write( message );

        string userInput = Console.ReadLine();

        while ( string.IsNullOrWhiteSpace( userInput ) )
        {
            PrintErrorMessage( errorMessage, message );

            userInput = Console.ReadLine();
        }

        return userInput.Trim();
    }

    public int GetValidProductQuantity( string message, string errorMessage )
    {
        Console.Write( message );

        string stringProductQuantity = Console.ReadLine();

        int productQuantity;

        while ( !int.TryParse( stringProductQuantity, out productQuantity ) || productQuantity <= 0 )
        {
            PrintErrorMessage( errorMessage, message );

            stringProductQuantity = Console.ReadLine();
        }

        return productQuantity;
    }

    public string GetValidUserCommandFromConsole( string message, string errorMessage )
    {
        Console.Write( message );

        string userInput = Console.ReadLine();

        while ( true )
        {
            userInput = userInput.Trim().ToLower();

            if ( Enum.IsDefined( typeof( UserCommands ), userInput ) )
            {
                return userInput.ToString().ToLower();
            }

            PrintErrorMessage( errorMessage, message );

            userInput = Console.ReadLine();
        }
    }

    private void PrintErrorMessage( string errorMessage, string message )
    {
        Console.WriteLine( errorMessage );
        Console.Write( message );
    }
}
