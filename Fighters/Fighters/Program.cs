using Fighters.Models.Fighters;
using Fighters.Utils;
using Fighters.Validator;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        ICustomValidator validator = new CustomValidator();

        Console.WriteLine( Messages.GreetingMessage );

        IFighter winner = GameManager.PlayGame( validator );

        Messages.PrintWinner( winner );
    }
}