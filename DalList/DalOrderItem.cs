
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
            if (id == DataSource.MyOrderItem[i]?.ID)
            {
                DataSource.MyOrderItem.RemoveAt(i);

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
    public void Update(OrderItem? o)
    {

        for (int i = 0; i < DataSource.MyOrderItem.Count; i++)
        {
            if (o?.ProductID == DataSource.MyOrderItem[i]?.ProductID)
            {
                DataSource.MyOrderItem[i] = o;
                return;
            }
        }

        throw new NotExistException("Not found Order item to Update");
    }

    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetByCondition(Func<OrderItem?, bool>? filter)
    {
        foreach (OrderItem? p in DataSource.MyOrderItem)
        {
            if (filter!(p))
            {
                return (OrderItem)p!;
            }
        }
        throw new DO.NotExistException("NOT exists!");
    }
    /// <summary>
    /// Returns All Order Item in the list.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        if (filter == null)
        {
            var list = from item in DataSource.MyOrderItem
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.MyOrderItem
                       where (filter(item))
                       select item;
            return list;
        }
    }  
   
}
