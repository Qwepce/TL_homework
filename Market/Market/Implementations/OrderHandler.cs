using Market.Interfaces;
using Market.Model;
using Market.Utils;

namespace Market.Implementations;

public class OrderHandler : IOrderHandler
{
    private readonly ICustomValidator _validator;

    private const int DELIVERY_DAYS = 3;

    public OrderHandler( ICustomValidator validator )
    {
        _validator = validator;
    }

    public bool? ConfirmOrder( string productName, int productQuantity, string customerName, string address )
    {
        var confirmationMessage = $"""
            Здравствуйте, {customerName}!
            Вы заказали {productName} в количестве {productQuantity} шт. по адресу {address}
            Все данные указаны верно? (yes/no/cancel)
            """;

        Console.WriteLine( $"\n{confirmationMessage}" );

        string userSelectedCommand = _validator.GetValidUserCommandFromConsole( Messages.ASK_USER_COMMAND_MESSAGE, Messages.UNKNOWN_SELECTED_COMMAND_ERROR_MESSAGE );

        return userSelectedCommand switch
        {
            "yes" => true,
            "no" => false,
            "cancel" => null
        };
    }

    public Order CreateOrder( string productName, int productQuantity, string customerName, string address )
    {
        DateTime deliveryDate = DateTime.Now.AddDays( DELIVERY_DAYS );

        Order order = new Order( productName, productQuantity, customerName, address, deliveryDate );

        return order;
    }

    public void SuccessfullOrderCreated( Order order )
    {
        var successfulMessage = $"""
            {order.CustomerName}!
            Ваш заказ {order.ProductName} в количестве {order.ProductQuantity} шт. оформлен!
            Ожидайте доставку по адресу {order.Address} {order.DeliveryDate:d MMMM yyyy}г.
            """;

        Console.WriteLine( $"\n{successfulMessage}" );
    }
}
