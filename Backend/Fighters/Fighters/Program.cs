using Fighters.ConsoleReader;
using Fighters.Factory;
using Fighters.Models.Fighters;
using Fighters.Utils;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        IConsoleInputReader consoleReader = new ConsoleInputReader();
        IFighterFactory fighterFactory = new FighterFactory(
            consoleReader,
            FighterOption.Races,
            FighterOption.Weapons,
            FighterOption.Armors,
            FighterOption.FighterClasses );
        IGameManager gameManager = new GameManager( fighterFactory, consoleReader );

        Console.WriteLine( Messages.GreetingMessage );

        IFighter winner = gameManager.PlayGame();

        Messages.PrintWinner( winner );
    }
}