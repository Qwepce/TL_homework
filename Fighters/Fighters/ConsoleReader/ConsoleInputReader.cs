using Fighters.Utils;

namespace Fighters.ConsoleReader;

public class ConsoleInputReader : IConsoleInputReader
{
    public int GetValidPositiveIntegerInput( int lowerLimit = 1 )
    {
        string userCommandInput = Console.ReadLine();

        int userCommandChoice;

        while ( !int.TryParse( userCommandInput, out userCommandChoice ) || userCommandChoice < lowerLimit )
        {
            Messages.PrintInvalidUserIntegerInput( lowerLimit );

            userCommandInput = Console.ReadLine();
        }

        return userCommandChoice;
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
