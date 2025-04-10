using Market.Model;

namespace Market.Utils;

public class Messages
{
    public const string GreetingMessage = "Добро пожаловать!";
    public const string FarewellMessage = "До свидания!";

    public const string InputProductNameMessage = "Введите название товара: ";
    public const string InputProductQuantityMessage = "Введите количество товара: ";
    public const string InputCustomerNameMessage = "Введите ваше имя: ";
    public const string InputAddressMessage = "Укажите адрес доставки: ";
    public const string AskUserCommandMessage = "Введите ваш ответ: ";

    public const string DefaultStringInputErrorMessage = "Ошибка, некорректно введённые данные.";
    public const string DefaultQuantityInputErrorMessage = "Ошибка, укажите целое положительное число.";
    public const string UnknownSelectedCommandErrorMessage = "Введена неизвестная команда.";

    public const string CancelOrderMessage = "Ваш заказ отменен.";

    public static void PrintOrderConfirmation( Order order )
    {
        string successfulMessage = $"""
            {order.CustomerName}!
            Ваш заказ {order.ProductName} в количестве {order.ProductQuantity} шт. оформлен!
            Ожидайте доставку по адресу {order.DeliveryAddress} {order.DeliveryDate:d MMMM yyyy}г.
            """;

        Console.WriteLine( $"\n{successfulMessage}" );
    }

    public static void PrintConfirmationMessage(
        string productName,
        int productQuantity,
        string customerName,
        string deliveryAddress )
    {
        string confirmationMessage = $"""
            Здравствуйте, {customerName}!
            Вы заказали {productName} в количестве {productQuantity} шт. по адресу {deliveryAddress}
            Все данные указаны верно? (yes/no/cancel)
            """;

        Console.WriteLine( $"\n{confirmationMessage}" );
    }
}