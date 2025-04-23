using Fighters.Factory;
using Fighters.Models.Fighters;
using Fighters.Utils;
using Fighters.Validator;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        ICustomValidator validator = new CustomValidator();
        IFighterFactory fighterFactory = new FighterFactory( validator );
        GameManager gameManager = new GameManager( fighterFactory, validator );

        Console.WriteLine( Messages.GreetingMessage );

        IFighter winner = gameManager.PlayGame();

        Messages.PrintWinner( winner );
    }
}