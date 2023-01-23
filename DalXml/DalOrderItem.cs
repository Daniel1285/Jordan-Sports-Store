using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    
    const string OrderItemPath = @"OrderItem"; //XML Serializer
    static OrderItem? createOrderItenFromXElement(XElement o)
    {
        return new OrderItem
        {
            ID = o.ToIntNullable("ID") ?? throw new FormatException("id"),
            ProductID = o.ToIntNullable("ProductID") ?? throw new FormatException("productID"),
            OrderID = o.ToIntNullable("OrderID") ?? throw new FormatException("orderID"),
            Price = o.ToDoubleNullable("Price") ?? throw new FormatException("price"),
            Amount = o.ToIntNullable("Amount") ?? throw new FormatException("amount"),

        };
    }

    #region Add orderItem
    /// <summary>
    /// Add object to xml file.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem o)
    {
        o.ID = XMLTools.Load_Config().ToIntNullable("OrderItemID")!.Value + 1;
        XMLTools.SaveConfigXml("OrderItemID", o.ID);
        XElement OrderItemRoot = XMLTools.LoadListFromXMLElement(OrderItemPath);
        XElement orderItem = new XElement("orderItem",
                                          new XElement("ID", o.ID),
                                          new XElement("ProductID", o.ProductID),
                                          new XElement("OrderID", o.OrderID),
                                          new XElement("Price", o.Price),
                                          new XElement("Amount", o.Amount));
        OrderItemRoot.Add(orderItem);
        XMLTools.SaveListToXMLElement(OrderItemRoot, OrderItemPath);    
        return o.ID;
    }
    #endregion

    #region Delete orderItem
    /// <summary>
    /// Delete OrderItem from Xml file.
    /// </summary>
    /// <param name="id"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement OrderItemRoot = XMLTools.LoadListFromXMLElement(OrderItemPath);
        XElement? orderItem = (from item in OrderItemRoot.Elements()
                               where item.ToIntNullable("ID") == id
                               select item).FirstOrDefault() ?? throw new NotExistException("Not found order item to delete");
        orderItem.Remove();
        XMLTools.SaveListToXMLElement(OrderItemRoot, OrderItemPath);
        
    }
    #endregion

    #region Update OrderItem
    /// <summary>
    /// Update object in xml file
    /// </summary>
    /// <param name="o"></param>
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Update (OrderItem o)
    {
        Delete(o.ID);
        Add(o);
    }
    #endregion

    #region get by condition
    /// <summary>
    /// get the object by condition
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotExistException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem GetByCondition(Func<OrderItem?, bool>? filter)
    {
        XElement? OrderItemRoot = XMLTools.LoadListFromXMLElement(OrderItemPath);
        return (from item in OrderItemRoot.Elements()
                let DOOrderItem = createOrderItenFromXElement(item)
                where filter!(DOOrderItem)
                select (OrderItem?)DOOrderItem).FirstOrDefault() ?? throw new NotExistException("NOT exists!");

    }
    #endregion

    #region gat list 
    /// <summary>
    /// get List or by filter or by nothing
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter)
    {
        XElement? OrderItemRoot = XMLTools.LoadListFromXMLElement(OrderItemPath);
        if(filter != null)
        {
            return from item in OrderItemRoot.Elements()
                   let tempItem = createOrderItenFromXElement(item)
                   where filter(tempItem)
                   select (OrderItem?)tempItem;
        }
        else
        {
            return from item in OrderItemRoot.Elements()
                   select createOrderItenFromXElement(item);
        }
    }
    #endregion

}
