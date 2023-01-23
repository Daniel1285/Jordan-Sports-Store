
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    #region Add orderItem
    /// <summary>
    /// Add a OrderItem to array "MyOrderItem" in DataSource and increases the size of the array "SizeOfOrderItem" by one.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem o)
    {
        o.ID = DataSource.Config.GetIdForOrderItem;
        DataSource.MyOrderItem.Add(o);
        return o.ID;
    }
    #endregion

    #region Delete orderItem
    /// <summary>
    /// Deleteing an Order Item from the array "MyOrderItem".
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        var orderItem1 = DataSource.MyOrderItem.Where(x => x?.ID == id).Select(x => x).FirstOrDefault();
        if(orderItem1 != null)
        {
            DataSource.MyOrderItem.Remove(orderItem1);
            return;
        }
        throw new NotExistException("Not found order item to delete");
    }
    #endregion

    #region Update orderItem
    /// <summary>
    /// Updates a Order Item by overwriting an existing Order Item.
    /// </summary>
    /// <param name="o"></param>(
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem o)
    {

        
        var orderItem1 = DataSource.MyOrderItem.Where(x => x?.ID == o.ID).Select(x => x).FirstOrDefault();
        if (orderItem1 != null)
        {
            DataSource.MyOrderItem.Remove(orderItem1);
            DataSource.MyOrderItem.Add(o);
            DataSource.MyOrderItem = DataSource.MyOrderItem.OrderBy(x => x?.ID).ToList();
            return;
        }
        throw new NotExistException("Not found Order item to Update");
    }
    #endregion

    #region Get OrderItem By Condition
    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetByCondition(Func<OrderItem?, bool>? filter)
    {
        
        var orderItem1 = DataSource.MyOrderItem.Where(filter!).Select(x => x).FirstOrDefault();
        if (orderItem1 != null)
            return (OrderItem)orderItem1;
        throw new DO.NotExistException("NOT exists!");
    }
    #endregion

    #region GetAll
    /// <summary>
    /// Returns All Order Item in the list.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    #endregion

}
