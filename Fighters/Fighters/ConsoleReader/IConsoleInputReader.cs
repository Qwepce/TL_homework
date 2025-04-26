namespace Fighters.ConsoleReader;

public interface IConsoleInputReader
{
    int GetValidPositiveIntegerInput( int lowerLimit = 1 );

    string GetValidUserStringInput();
}
