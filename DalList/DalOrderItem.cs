
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
    public int Add(OrderItem o)
    {
        o.ID = DataSource.Config.GetIdForOrderItem;
        DataSource.MyOrderItem.Add(o);
        return o.ProductID;
    }


    /// <summary>
    /// Deleteing an Order Item from the array "MyOrderItem".
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.MyOrderItem.Count; i++)
        {
            if (id == DataSource.MyOrderItem[i]?.OrderID)
            {
                DataSource.MyOrderItem[i] = DataSource.MyOrderItem[DataSource.MyOrderItem.Count];
                Console.WriteLine("sucssce");
                return;
            }
        }
        throw new NotExistException("Not found order item to delete");
    }


    /// <summary>
    /// Updates a Order Item by overwriting an existing Order Item.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem o)
    {

        for (int i = 0; i < DataSource.MyOrderItem.Count; i++)
        {
            if (o.ProductID == DataSource.MyOrderItem[i]?.ProductID)
            {
                DataSource.MyOrderItem[i] = o;
                return;
            }
        }

        throw new NotExistException("Not found Order item to Update");
    }

    /// <summary>
    /// Returns a Order Item by ID number of order.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetByID(int orderId)
    {
        foreach (OrderItem o in DataSource.MyOrderItem)
        {
            if ( orderId == o.OrderID)
            {
                return o;
            }

        }
        throw new NotExistException("Order item not found");
    }


    /// <summary>
    /// Returns a Order Item by ID number of product and order.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetByTwoID(int orderId, int productID)
    {
        foreach (OrderItem o in DataSource.MyOrderItem)
        {
            if (orderId == o.OrderID && productID == o.ProductID)
            {
                return o;
            }
        }
        throw new NotExistException("Order item not found");
    }



    /// <summary>
    /// Returns All Order Item in the list.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        List<OrderItem?> newOrdersItem = new List<OrderItem?>();
        for (int i = 0; i < DataSource.MyOrderItem.Count; i++)
        {
            OrderItem o = new OrderItem();
            o = (OrderItem)DataSource.MyOrderItem[i];
            newOrdersItem.Add(o);
        }

        return newOrdersItem;
    }






    /// <summary>
    /// Method of reading a list/array of order details according to the ID number of the order.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public List<OrderItem?> GetOrdersItem(int orderID)
    {
        List<OrderItem?> ArrOrders = new List<OrderItem?>();
        foreach (DO.OrderItem item in DataSource.MyOrderItem)
        {
            if (item.OrderID == orderID)
            {
                ArrOrders.Add(item);
            }
        }
        
        return ArrOrders;   

    }

}
