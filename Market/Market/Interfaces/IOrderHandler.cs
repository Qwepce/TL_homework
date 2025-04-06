using Market.Model;

namespace Market.Interfaces;

public interface IOrderHandler
{
    bool? ConfirmOrder( string productName, int productQuantity, string customerName, string address );

    Order CreateOrder( string productName, int productQuantity, string customerName, string address );

    void SuccessfullOrderCreated( Order order );
}
