
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
    public int Add(Order o)
    {
        DataSource.MyOrder.Add(o);
        return o.ID;
    }

    /// <summary>
    /// Deleteing a Order from the array "MyOrder".
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {

        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            if (id == DataSource.MyOrder[i].ID)
            {
                DataSource.MyOrder.Remove(DataSource.MyOrder[i]);  
                Console.WriteLine("sucsses");
                return;
            }
        }
        throw new NotExistException("Not found order to delete");
    }

    /// <summary>
    /// Updates a Order by overwriting an existing Order.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            if (o.ID == DataSource.MyOrder[i].ID)
            {
                DataSource.MyOrder[i] = o;
                return;
            }
        }
        throw new NotExistException("Not found order to Update! ");
    }

    /// <summary>
    /// Returns a Order by ID number.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetByID(int id)
    {
        foreach (Order o  in DataSource.MyOrder)
        {
            if (id == o.ID)
            {
                return o;
            }
        }
        throw new NotExistException("Order are not found!");
    }

    /// <summary>
    /// Returns All Orders in the array.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order>? GetAll()
    {
        List<Order> GetOrders = new List<Order>();
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            Order o = new Order();
            o = DataSource.MyOrder[i];
            GetOrders.Add(o);
            
        }
        return GetOrders;   
    }

 
}


