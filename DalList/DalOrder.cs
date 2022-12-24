
using DO;
namespace Dal;
using DalApi;
using System.Security.Cryptography.X509Certificates;

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

        var order1 =(from item in DataSource.MyOrder
                    where(item?.ID == id)
                    select item).FirstOrDefault();

        if (order1 != null)
        {
            DataSource.MyOrder.Remove(order1);
            return;
        }
        throw new NotExistException();
    }

    /// <summary>
    /// Updates a Order by overwriting an existing Order.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {

        var order1 = (from item in DataSource.MyOrder
                      where (item?.ID == o.ID)
                      select item).FirstOrDefault();
        if (order1 != null)
        {
            int index = DataSource.MyOrder.IndexOf(order1);
            DataSource.MyOrder.Remove(order1);
            DataSource.MyOrder.Insert(index,o);
            //DataSource.MyOrder = DataSource.MyOrder.OrderBy(x => x?.ID).ToList();
            return;
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
        
        var order = (from item in DataSource.MyOrder
                    where(filter!(item))
                    select (item)).FirstOrDefault();
        if (order != null)
            return (Order)order;
        throw new NotExistException("NOT exists!");
    }




}


