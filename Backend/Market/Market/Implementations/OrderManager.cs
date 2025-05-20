using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market.Implementations;

public class OrderManager : IOrderManager
{
    private readonly ICustomValidator _validator;

    private const int DeliveryDays = 3;

    public OrderManager( ICustomValidator validator )
    {
        _validator = validator;
    }

    public void ProcessOrder()
    {
        bool isOrderCreatedOrCancelled = false;

        while ( !isOrderCreatedOrCancelled )
        {
            string productName = _validator.GetValidInputFromConsole( Messages.InputProductNameMessage, Messages.DefaultStringInputErrorMessage );
            int productQuantity = _validator.GetValidProductQuantityFromConsole( Messages.InputProductQuantityMessage, Messages.DefaultQuantityInputErrorMessage );
            string customerName = _validator.GetValidInputFromConsole( Messages.InputCustomerNameMessage, Messages.DefaultStringInputErrorMessage );
            string deliveryAddress = _validator.GetValidInputFromConsole( Messages.InputAddressMessage, Messages.DefaultStringInputErrorMessage );

            Messages.PrintConfirmationMessage(
                productName,
                productQuantity,
                customerName,
                deliveryAddress );

            UserCommand selectedUserCommand = RequestUserComfirmationCommand();

            while ( selectedUserCommand.Equals( UserCommand.UnknownCommand ) )
            {
                Console.WriteLine( Messages.UnknownSelectedCommandErrorMessage );
                selectedUserCommand = RequestUserComfirmationCommand();
            }

            switch ( selectedUserCommand )
            {
                case UserCommand.Yes:
                    Order order = CreateOrder( productName, productQuantity, customerName, deliveryAddress );

                    Messages.PrintOrderConfirmation( order );

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

    private UserCommand RequestUserComfirmationCommand()
    {
        UserCommand selectedCommand;

        string userSelectedCommand = _validator.GetValidUserCommandFromConsole( Messages.AskUserCommandMessage, Messages.UnknownSelectedCommandErrorMessage );

        switch ( userSelectedCommand )
        {
            case "yes":
                selectedCommand = UserCommand.Yes;
                break;

            case "no":
                selectedCommand = UserCommand.No;
                break;

            case "cancel":
                selectedCommand = UserCommand.Cancel;
                break;

            default:
                selectedCommand = UserCommand.UnknownCommand;
                break;
        }

        return selectedCommand;
    }

    private Order CreateOrder(
        string productName,
        int productQuantity,
        string customerName,
        string deliveryAddress )
    {
        DateTime deliveryDate = DateTime.Now.AddDays( DeliveryDays );

        Order order = new Order( productName, productQuantity, customerName, deliveryAddress, deliveryDate );

        return order;
    }

}
