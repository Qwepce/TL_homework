namespace Dictionary.Interfaces;

public interface ICustomValidator
{
    int GetValidUserChoice();

    string GetValidUserInput( string askUserInput );
}
