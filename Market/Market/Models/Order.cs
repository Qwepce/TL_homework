namespace Market.Model;

public class Order
{
    public string ProductName { get; init; }

    public int ProductQuantity { get; init; }

    public string CustomerName { get; init; }

    public string Address { get; init; }

    public DateTime DeliveryDate { get; init; }

    public Order(
        string productName,
        int productQuantity,
        string customerName,
        string address,
        DateTime deliveryDate )
    {
        ProductName = productName;
        ProductQuantity = productQuantity;
        CustomerName = customerName;
        Address = address;
        DeliveryDate = deliveryDate;
    }
}