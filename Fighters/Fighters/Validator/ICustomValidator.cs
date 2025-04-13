namespace Fighters.Validator;

public interface ICustomValidator
{
    int GetPositiveIntegerInput( string errorMessage );

    string GetValidUserInput( string message );
}
