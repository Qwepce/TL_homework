namespace Market.Model;

public class Order
{
    public string ProductName { get; init; }

    public int ProductQuantity { get; init; }

    public string CustomerName { get; init; }

    public string DeliveryAddress { get; init; }

    public DateTime DeliveryDate { get; init; }

    public Order(
        string productName,
        int productQuantity,
        string customerName,
        string deliveryAddress,
        DateTime deliveryDate )
    {
        ProductName = productName;
        ProductQuantity = productQuantity;
        CustomerName = customerName;
        DeliveryAddress = deliveryAddress;
        DeliveryDate = deliveryDate;
    }
}