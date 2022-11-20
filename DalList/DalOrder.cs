
using DO;
namespace Dal;
using DalApi;

internal class DalOrder: IOrder
{
    /// <summary>
    /// Add a Order to array "MyOrder" in DataSource and increases the size of the array "SizeOfOrder" by one.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public int  AddNewOrder (Order o)
    {
        DataSource.MyOrder.Add(o);
        return o.ID;

    }

    /// <summary>
    /// Deleteing a Order from the array "MyOrder".
    /// </summary>
    /// <param name="id"></param>
    public void DeleteOrder(int id)
    {
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            if (id == DataSource.MyOrder[i].ID)
            {
                DataSource.MyOrder[i] = DataSource.MyOrder[DataSource.MyOrder.Count];
                Console.WriteLine("sucsses");
            }
        }
    }

    /// <summary>
    /// Updates a Order by overwriting an existing Order.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateOrder(ref Order o)
    {
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            if (o.ID == DataSource.MyOrder[i].ID)
            {
                DataSource.MyOrder[i] = o;
                return;
            }
        }
        throw new Exception("Not found order to Update! ");
    }

    /// <summary>
    /// Returns a Order by ID number.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetOrder(int id)
    {
        foreach (Order o  in DataSource.MyOrder)
        {
            if (id == o.ID)
            {
                return o;
            }
        }
        throw new Exception("Order are not found!");
    }

    /// <summary>
    /// Returns All Orders in the array.
    /// </summary>
    /// <returns></returns>
    public List<Order> GetAllOrders()
    {
        List<Order> GetOrders = new List<Order>();
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            Order o = new Order();
            o = DataSource.MyOrder[i];
            GetOrders[i] = o;
            
        }
        return GetOrders;   
    }

 
}


