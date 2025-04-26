using Fighters.Utils;

namespace Fighters.ConsoleReader;

public class ConsoleInputReader : IConsoleInputReader
{
    public int GetValidPositiveIntegerInput( int lowerLimit = 1 )
    {
        string userInput = Console.ReadLine();

        int parsedIntegerFromUserInput;

        while ( !int.TryParse( userInput, out parsedIntegerFromUserInput ) || parsedIntegerFromUserInput < lowerLimit )
        {
            Messages.PrintInvalidUserIntegerInput( lowerLimit );

            userInput = Console.ReadLine();
        }

        return parsedIntegerFromUserInput;
    }

    public string GetValidUserStringInput()
    {
        string userInput;

        while ( string.IsNullOrEmpty( userInput = Console.ReadLine()?.Trim() ) )
        {
            Console.Write( Messages.InvalidUserInput );
        }

        return userInput;
    }
}
