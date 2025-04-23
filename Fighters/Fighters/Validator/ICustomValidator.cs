using Fighters.Utils;

namespace Fighters.Validator;

public interface ICustomValidator
{
    int GetPositiveIntegerInput( int lowerLimit = 1 );

    string GetValidUserInput();
}
