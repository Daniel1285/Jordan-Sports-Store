using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    XElement? OrderItemRoot;
    const string OrderItemPath = @"OrderItem"; //XML Serializer
   
    #region LoadData
    /// <summary>
    /// Load data.
    /// </summary>
    private void LoadData()
    {
        try
        {
            OrderItemRoot = XElement.Load(OrderItemPath);
        }
        catch (Exception) { Console.WriteLine("File upload problem!");}
    }
    #endregion

    #region AddFunction
    /// <summary>
    /// Add 
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public int Add(OrderItem o)
    {
        XElement OrderItemID = new XElement ("ID", o.ID); 
        XElement ProductID = new XElement ("ProductID", o.ProductID); 
        XElement OrderID = new XElement ("OrderID", o.OrderID); 
        XElement Price = new XElement ("Price", o.Price); 
        XElement Amount = new XElement ("Amount", o.Amount);

        OrderItemRoot?.Add(new XElement("OrderItem",OrderItemID, ProductID, OrderID, Price, Amount));
        OrderItemRoot?.Save(OrderItemPath);
        return o.ID;
    }
    #endregion

    #region DeleteFunction
    /// <summary>
    /// Delete OrderItem from Xml file.
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id)
    {
        XElement? OrderItemElement;

        
        OrderItemElement = (from OrderItems in OrderItemRoot?.Elements()
                            where Convert.ToInt32(OrderItems.Element("ID")!.Value!) == id
                            select OrderItems).FirstOrDefault();

        if (OrderItemElement != null)
        {
            OrderItemElement.Remove();
            OrderItemRoot?.Save(OrderItemPath);
        }
        else
            throw new NotExistException("Not found order item to delete");
         
    }
    #endregion

}
