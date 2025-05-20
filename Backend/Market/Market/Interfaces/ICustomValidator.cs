namespace Market.Interfaces;

public interface ICustomValidator
{
    string GetValidInputFromConsole( string message, string errorMessage );

    int GetValidProductQuantityFromConsole( string message, string errorMessage );

    string GetValidUserCommandFromConsole( string message, string errorMessage );
}
