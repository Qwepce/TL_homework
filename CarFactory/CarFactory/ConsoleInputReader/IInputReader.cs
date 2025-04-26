using CarFactory.Enums;

namespace CarFactory.ConsoleInputReader;

public interface IInputReader
{
    int GetValidUserOption();

    UserCommand GetValidUserCommand();
}
