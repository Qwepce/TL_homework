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

    public int GetValidProductQuantityFromConsole( string message, string errorMessage )
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

        while ( true )
        {
            string userInput = Console.ReadLine()?.Trim();

            if ( int.TryParse( userInput, out int _ ) )
            {
                PrintErrorMessage( errorMessage, message );
                continue;
            }

            if ( Enum.TryParse( typeof( UserCommand ), userInput, ignoreCase: true, out object? command ) )
            {
                return ( ( UserCommand )command ).ToString().ToLower();
            }

            PrintErrorMessage( errorMessage, message );
        }
    }

    private void PrintErrorMessage( string errorMessage, string message )
    {
        Console.WriteLine( errorMessage );
        Console.Write( message );
    }
}
