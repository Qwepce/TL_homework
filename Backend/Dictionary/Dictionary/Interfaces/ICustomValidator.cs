namespace Dictionary.Interfaces;

public interface ICustomValidator
{
    int GetValidUserCommand();

    string GetValidUserInput( string askUserInput );
}
