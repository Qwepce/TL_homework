using CarFactory.Enums;

namespace CarFactory.ConsoleReader;

public interface IInputReader
{
    int GetValidUserOption();

    UserCommand GetValidUserCommand();
}
