using CarFactory.ConsoleReader;
using CarFactory.Factory;
using CarFactory.Utils;

namespace CarFactory;

public class Program
{
    public static void Main()
    {
        IInputReader consoleInputReader = new ConsoleInputReader();
        ICarsFactory carsFactory = new CarsFactory( consoleInputReader );
        ICarManager carManager = new CarManager( consoleInputReader, carsFactory );

        Console.WriteLine( Messages.GreetingMessage );

        carManager.Run();

        Console.WriteLine( Messages.FarewellMessage );
    }
}