using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market.Implementations;

public class OrderHandler : IOrderHandler
{
    private readonly ICustomValidator _validator;

    private const int DeliveryDays = 3;

    public OrderHandler( ICustomValidator validator )
    {
        _validator = validator;
    }

    public UserCommand ConfirmOrder(
        string productName,
        int productQuantity,
        string customerName,
        string address )
    {
        string confirmationMessage = $"""
            Здравствуйте, {customerName}!
            Вы заказали {productName} в количестве {productQuantity} шт. по адресу {address}
            Все данные указаны верно? (yes/no/cancel)
            """;

        Console.WriteLine( $"\n{confirmationMessage}" );

        string userSelectedCommand = _validator.GetValidUserCommandFromConsole( Messages.AskUserCommandMessage, Messages.UnknownSelectedCommandErrorMessage );

        return userSelectedCommand switch
        {
            "yes" => UserCommand.Yes,
            "no" => UserCommand.No,
            "cancel" => UserCommand.Cancel
        };
    }

    public Order CreateOrder(
        string productName,
        int productQuantity,
        string customerName,
        string address )
    {
        DateTime deliveryDate = DateTime.Now.AddDays( DeliveryDays );

        Order order = new Order( productName, productQuantity, customerName, address, deliveryDate );

        return order;
    }

    public void PrintOrderConfirmation( Order order )
    {
        string successfulMessage = $"""
            {order.CustomerName}!
            Ваш заказ {order.ProductName} в количестве {order.ProductQuantity} шт. оформлен!
            Ожидайте доставку по адресу {order.DeliveryAddress} {order.DeliveryDate:d MMMM yyyy}г.
            """;

        Console.WriteLine( $"\n{successfulMessage}" );
    }
}
