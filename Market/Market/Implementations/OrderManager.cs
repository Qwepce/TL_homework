using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market.Implementations;

public class OrderManager : IOrderManager
{
    private readonly ICustomValidator _validator;

    private readonly IOrderHandler _orderHandler;

    public OrderManager( ICustomValidator validator, IOrderHandler handler )
    {
        _validator = validator;
        _orderHandler = handler;
    }

    public void InitOrderManager()
    {
        bool isOrderCreatedOrCancelled = false;

        while ( !isOrderCreatedOrCancelled )
        {
            string productName = _validator.GetValidInputFromConsole( Messages.InputProductNameMessage, Messages.DefaultStringInputErrorMessage );
            int productQuantity = _validator.GetValidProductQuantity( Messages.InputProductQuantityMessage, Messages.DefaultQuantityInputErrorMessage );
            string customerName = _validator.GetValidInputFromConsole( Messages.InputCustomerNameMessage, Messages.DefaultStringInputErrorMessage );
            string address = _validator.GetValidInputFromConsole( Messages.InputAddressMessage, Messages.DefaultStringInputErrorMessage );

            UserCommand selectedUserCommand = _orderHandler.ConfirmOrder( productName, productQuantity, customerName, address );

            switch ( selectedUserCommand )
            {
                case UserCommand.Yes:
                    Order order = _orderHandler.CreateOrder( productName, productQuantity, customerName, address );
                    _orderHandler.PrintOrderConfirmation( order );

                    isOrderCreatedOrCancelled = true;
                    break;

                case UserCommand.No:
                    break;

                case UserCommand.Cancel:
                    Console.WriteLine( Messages.CancelOrderMessage );
                    isOrderCreatedOrCancelled = true;
                    break;
            }
        }
    }
}
