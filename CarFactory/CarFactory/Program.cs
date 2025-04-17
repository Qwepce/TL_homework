using CarFactory.Utils;

namespace CarFactory;

public class Program
{
    public static void Main()
    {
        CarManager carManager = new CarManager();

        Console.WriteLine( Messages.GreetingMessage );

        carManager.Run();

        Console.WriteLine( Messages.FarewellMessage );
    }
}