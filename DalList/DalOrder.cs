
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
        o.ID = DataSource.Config.GetIdForOrder;
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
            if (id == DataSource.MyOrder[i]?.ID)
            {
                DataSource.MyOrder.Remove(DataSource.MyOrder[i]);  
                Console.WriteLine("sucsses");
                return;
            }
        }
        throw new NotExistException();
    }

    /// <summary>
    /// Updates a Order by overwriting an existing Order.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order? o)
    {
        for (int i = 0; i < DataSource.MyOrder.Count; i++)
        {
            if (o?.ID == DataSource.MyOrder[i]?.ID)
            {
                
                DataSource.MyOrder[i] = o;
                return;
            }
        }
        throw new NotExistException("Not found order to Update!");
    }

    /// <summary>
    /// Returns All Orders in the array.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {
        if (filter == null)
        {
            var list = from item in DataSource.MyOrder
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.MyOrder
                       where (filter(item))
                       select item;
            return list;
        }
    }
    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistException"></exception>
    public Order GetByCondition(Func<Order?, bool>? filter)
    {
        foreach (Order? p in DataSource.MyOrder)
        {
            if (filter!(p))
            {
                return (Order)p!;
            }
        }
        throw new DO.NotExistException("NOT exists!");
    }




}


