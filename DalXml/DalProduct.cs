using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct :IProduct
{ 
    const string s_product = @"Product"; // XML Serializer

    #region Add product
    /// <summary>
    /// Add product to xml product.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int Add(Product p)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        if (products.FirstOrDefault(x => x?.ID == p.ID) != null)
            throw new AlreadyExistException("id alraeady exist"); 

        products.Add(p);
        XMLTools.SaveListToXMLSerializer(products, s_product);

        return p.ID;
    }
    #endregion

    #region Delete product
    /// <summary>
    /// Delete product from xml.
    /// </summary>
    /// <param name="productID"></param>
    public void Delete(int productID)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        if (products.RemoveAll(x => x?.ID == productID) == 0)
            throw new NotExistException("Not found Product to delete");

        XMLTools.SaveListToXMLSerializer(products, s_product);
    }
    #endregion

    #region Update product
    /// <summary>
    /// Update product to xml.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="NotExistException"></exception>
    public void Update(Product p)
    {
        Delete(p.ID);
        Add(p);
    }
    #endregion

    #region Get by condition
    /// <summary>
    /// Get order grom xml by condition.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="NotExistException"></exception>
    public Product GetByCondition(Func<Product?, bool>? filter)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        return products.FirstOrDefault(filter!) ?? throw new NotExistException("Not exsist");
    }
    #endregion

    #region Get all
    /// <summary>
    /// Get all from xml.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        if (filter == null)
        {
            var list = from item in products
                       select item;
            return list;
        }
        else
        {
            var list = from item in products
                       where (filter(item))
                       select item;
            return list;
        }
    }
    #endregion
}
