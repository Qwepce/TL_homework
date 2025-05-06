using Fighters.Utils;

namespace Fighters.ConsoleReader;

public class ConsoleInputReader : IConsoleInputReader
{
    public int GetValidPositiveIntegerInput( int lowerLimit = 1 )
    {
        string userInput = ReadLine();

        int parsedIntegerFromUserInput;

        while ( !int.TryParse( userInput, out parsedIntegerFromUserInput ) || parsedIntegerFromUserInput < lowerLimit )
        {
            Messages.PrintInvalidUserIntegerInput( lowerLimit );

            userInput = ReadLine();
        }

        return parsedIntegerFromUserInput;
    }

    public string GetValidUserStringInput()
    {
        string userInput;

        while ( string.IsNullOrEmpty( userInput = ReadLine()?.Trim() ) )
        {
            Console.Write( Messages.InvalidUserInput );
        }

        return userInput;
    }

    internal virtual string ReadLine() => Console.ReadLine();
}
