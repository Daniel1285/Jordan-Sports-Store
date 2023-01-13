using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrder : IOrder
{
    const string s_Order = @"Order";

    #region Add order
    /// <summary>
    /// Add a Order to xml file
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public int Add(Order o)
    {
        o.ID = XMLTools.Load_Config().ToIntNullable("OrderID")!.Value + 1;
        XMLTools.SaveConfigXml("OrderID", o.ID);
        List<Order?> list = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);
        list.Add(o);
        XMLTools.SaveListToXMLSerializer(list, s_Order);
        return o.ID;
    }
    #endregion

    #region Delete order
    /// <summary>
    /// Deleteing a Order from the the xml file Order.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        List<Order?> list = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);
        if(list.RemoveAll(x => x?.ID == id) == 0)
        {
            throw new NotExistException();
        }
        XMLTools.SaveListToXMLSerializer(list, s_Order);
    }
    #endregion

    #region Update order
    /// <summary>
    /// Updates a Order by overwriting an existing Order in xml file.
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order o)
    {
        Delete(o.ID);
        Add(o);
    }
    #endregion

    #region Get all
    /// <summary>
    /// Returns All Orders in the file.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter)
    {
        IEnumerable<Order?> list = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);
        if(filter == null)
        {
            list = from item in list
                   select item;
            return list;
        }
        else
        {
            list = from item in list
                   where (filter(item))
                   select item;
            return list;
        }
    }
    #endregion

    #region Get by condiyion
    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="DO.NotExistException"></exception>
    public Order GetByCondition(Func<Order?, bool>? filter)
    {
        IEnumerable<Order?> list = XMLTools.LoadListFromXMLSerializer<Order>(s_Order);
        var order = list.FirstOrDefault(filter!);
        if(order != null)
        {
            return (Order)order;
        }
        throw new NotExistException("NOT exists!");
    }
    #endregion
}
