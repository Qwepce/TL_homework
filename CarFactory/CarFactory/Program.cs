using CarFactory.Factory;
using CarFactory.Utils;
using CarFactory.Utils.UserInputReader;

namespace CarFactory;

public class Program
{
    public static void Main()
    {
        IInputReader inputReader = new InputReader();
        ICarFactory carFactory = new Factory.CarFactory( inputReader );
        ICarManager carManager = new CarManager( inputReader, carFactory );

        Console.WriteLine( Messages.GreetingMessage );

        carManager.Run();

        Console.WriteLine( Messages.FarewellMessage );
    }
}