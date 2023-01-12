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
    const string s_product = @"Product"; //XML Serializer
    /// <summary>
    ///
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int Add(Product p)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);  

    }
}
