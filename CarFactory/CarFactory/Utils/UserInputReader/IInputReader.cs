using CarFactory.Enums;

namespace CarFactory.Utils.UserInputReader;

public interface IInputReader
{
    int GetValidUserOption();

    UserCommand GetValidUserCommand();
}
