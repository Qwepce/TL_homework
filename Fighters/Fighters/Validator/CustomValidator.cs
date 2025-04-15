using Fighters.Utils;

namespace Fighters.Validator;

public class CustomValidator : ICustomValidator
{
    public int GetPositiveIntegerInput()
    {
        string userCommandInput = Console.ReadLine();

        int userCommandChoice;

        while ( !int.TryParse( userCommandInput, out userCommandChoice ) || userCommandChoice <= 0 )
        {
            Console.Write( Messages.InvalidUserNumberInput );

            userCommandInput = Console.ReadLine();
        }

        return userCommandChoice;
    }

    public string GetValidUserInput()
    {
        string userInput;

        while ( string.IsNullOrEmpty( userInput = Console.ReadLine()?.Trim() ) )
        {
            Console.Write( Messages.InvalidUserInput );
        }

        return userInput;
    }
}
