using Market.Implementations;
using Market.Interfaces;
using Market.Utils;

namespace Market;

public class Program
{
    public static void Main()
    {
        ICustomValidator validator = new CustomValidator();

        IOrderManager orderManager = new OrderManager( validator );

        Console.WriteLine( Messages.GreetingMessage );

        orderManager.ProcessOrder();

        Console.WriteLine( Messages.FarewellMessage );
    }
}