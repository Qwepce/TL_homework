using Market.Implementations;
using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market;

public class Program
{
    public static void Main( string[] args )
    {
        Console.WriteLine( Messages.GreetingMessage );

        InitOrderManager();

        Console.WriteLine( Messages.FarewellMessage );
    }

    public static void InitOrderManager()
    {
        ICustomValidator validator = new CustomValidator();
        IOrderHandler orderHandler = new OrderHandler( validator );

        while ( true )
        {
            string productName = validator.GetValidInputFromConsole( Messages.InputProductNameMessage, Messages.DefaultStringInputErrorMessage );
            int productQuantity = validator.GetValidProductQuantity( Messages.InputProductQuantityMessage, Messages.DefaultQuantityInputErrorMessage );
            string customerName = validator.GetValidInputFromConsole( Messages.InputCustomerNameMessage, Messages.DefaultStringInputErrorMessage );
            string address = validator.GetValidInputFromConsole( Messages.InputAddressMessage, Messages.DefaultStringInputErrorMessage );

            bool? isOrderConfirmed = orderHandler.ConfirmOrder( productName, productQuantity, customerName, address );

            if ( isOrderConfirmed == true )
            {
                Order order = orderHandler.CreateOrder( productName, productQuantity, customerName, address );
                orderHandler.SuccessfullOrderCreated( order );
                break;
            }
            else if ( isOrderConfirmed == null )
            {
                Console.WriteLine( Messages.CancelOrderMessage );
                break;
            }
        }
    }
}