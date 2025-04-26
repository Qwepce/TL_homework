using CarFactory.ConsoleInputReader;
using CarFactory.Factory;
using CarFactory.Utils;

namespace CarFactory;

public class Program
{
    public static void Main()
    {
        IInputReader consoleInputReader = new InputReader();
        ICarFactory carFactory = new Factory.CarFactory( consoleInputReader );
        ICarManager carManager = new CarManager( consoleInputReader, carFactory );

        Console.WriteLine( Messages.GreetingMessage );

        carManager.Run();

        Console.WriteLine( Messages.FarewellMessage );
    }
}