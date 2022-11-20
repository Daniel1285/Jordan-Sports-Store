
using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Add a OrderItem to array "MyOrderItem" in DataSource and increases the size of the array "SizeOfOrderItem" by one.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public int AddNewOrderItem(OrderItem o)
    {
        DataSource.MyOrderItem[DataSource.Config.SizeOfOrderItem++] = o;
        return o.ProductID;
    }

    /// <summary>
    /// Deleteing an Order Item from the array "MyOrderItem".
    /// </summary>
    /// <param name="id"></param>
    public void DeleteOrderItem(int id)
    {
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (id == DataSource.MyOrderItem[i].OrderID)
            {
                DataSource.MyOrderItem[i] = DataSource.MyOrderItem[DataSource.Config.SizeOfOrderItem--];
            }
        }
    }

    /// <summary>
    /// Updates a Order Item by overwriting an existing Order Item.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateOrderItem(ref OrderItem o)
    {

        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (o.ProductID == DataSource.MyOrderItem[i].ProductID)
            {
                DataSource.MyOrderItem[i] = o;
                return;
            }
        }

        throw new Exception("Not found Order item to Update");
    }

    /// <summary>
    /// Returns a Order Item by ID number of product and order.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItem(int prodctId, int orderId)
    {
        foreach (OrderItem o in DataSource.MyOrderItem)
        {
            if (prodctId == o.ProductID && orderId == o.OrderID)
            {
                return o;
            }

        }
        throw new Exception("Order item not found");
    }

    /// <summary>
    /// Returns All Order Item in the array.
    /// </summary>
    /// <returns></returns>
    public  OrderItem[] GetAllOrdersItem()
    {
        OrderItem[] newOrdersItem = new OrderItem[DataSource.MyOrderItem.Length];
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            OrderItem o = new OrderItem();
            o = DataSource.MyOrderItem[i];
            newOrdersItem[i] = o;
        }

        return newOrdersItem;
    }

    /// <summary>
    /// Method of reading a list/array of order details according to the ID number of the order.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public OrderItem[] GetOrdersItem(int orderID)
    {
        OrderItem[] ArrOrders = new OrderItem[DataSource.Config.SizeOfOrderItem];
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (orderID == DataSource.MyOrderItem[i].OrderID)
            {
                ArrOrders[i] = DataSource.MyOrderItem[i];
            }
        }
        return ArrOrders;   

    }

}
