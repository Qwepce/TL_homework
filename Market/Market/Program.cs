using Market.Implementations;
using Market.Interfaces;
using Market.Utils;

namespace Market;

public class Program
{
    public static void Main()
    {
        ICustomValidator validator = new CustomValidator();
        IOrderHandler orderHandler = new OrderHandler( validator );

        IOrderManager orderManager = new OrderManager( validator, orderHandler );

        Console.WriteLine( Messages.GreetingMessage );

        orderManager.InitOrderManager();

        Console.WriteLine( Messages.FarewellMessage );
    }
}