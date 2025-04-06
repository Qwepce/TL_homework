using Market.Implementations;
using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market;

public class OrderManager
{
    public static void Main( string[] args )
    {
        Console.WriteLine( Messages.GREETING_MESSAGE );

        InitOrderManager();

        Console.WriteLine( Messages.FAREWELL_MESSAGE );
    }

    public static void InitOrderManager()
    {
        ICustomValidator validator = new CustomValidator();
        IOrderHandler orderHandler = new OrderHandler( validator );

        while ( true )
        {
            string productName = validator.GetValidInputFromConsole( Messages.INPUT_PRODUCT_NAME_MESSAGE, Messages.DEFAULT_STRING_INPUT_ERROR_MESSAGE );
            int productQuantity = validator.GetValidProductQuantity( Messages.INPUT_PRODUCT_QUANTITY_MESSAGE, Messages.DEFAULT_QUANTITY_INPUT_ERROR_MESSAGE );
            string customerName = validator.GetValidInputFromConsole( Messages.INPUT_CUSTOMER_NAME_MESSAGE, Messages.DEFAULT_STRING_INPUT_ERROR_MESSAGE );
            string address = validator.GetValidInputFromConsole( Messages.INPUT_ADDRESS_MESSAGE, Messages.DEFAULT_STRING_INPUT_ERROR_MESSAGE );

            bool? isOrderConfirmed = orderHandler.ConfirmOrder( productName, productQuantity, customerName, address );

            if ( isOrderConfirmed == true )
            {
                Order order = orderHandler.CreateOrder( productName, productQuantity, customerName, address );
                orderHandler.SuccessfullOrderCreated( order );
                break;
            }
            else if ( isOrderConfirmed == null )
            {
                Console.WriteLine( Messages.CANCEL_ORDER_MESSAGE );
                break;
            }
        }
    }
}