using BlApi;

using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace BlImplementation;

/// <summary>
/// Implementation of product functions of BO->product
/// </summary>
internal class Product : BlApi.IProduct
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    #region Get product list
    /// <summary>
    /// We will ask DO for a list of products and build a new list of ProductForList  
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetProductList()
    {
       
        
        var productsForList = from DOProduct in Dal?.Product.GetAll()
                              select new BO.ProductForList()
                              {
                                  ID = (int)DOProduct?.ID!,
                                  Name = DOProduct?.Name,
                                  Price = (double)DOProduct?.Price!,
                                  Category = (BO.Enums.Category)DOProduct?.Category!
                              };
        return productsForList;
    }
    #endregion

    #region Get Product
    /// <summary>
    /// The function builds a new product BO by the received data
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdSmallThanZeroException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product GetProduct(int id)
    {
        BO.Product? p1 = null;
        if (id > 0)
        {
            try
            {
                DO.Product? p = new DO.Product();
                p = Dal?.Product.GetByCondition(x => x?.ID == id);

                p1 = new BO.Product
                {
                    ID = (int)p?.ID!,
                    Name = p?.Name,
                    Price = (double)p?.Price!,
                    Category = (BO.Enums.Category)p?.Category!,
                    InStock = (int)p?.InStock!,
                };
               
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex);}
            return p1;
        }

        else
            throw new BO.IdSmallThanZeroException("ID small than zero!");

    }
    #endregion

    #region Get product
    /// <summary>
    /// The function builds a new product item BO by the received data
    /// </summary>
    /// <param name="c"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdSmallThanZeroException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem GetProduct(BO.Cart c, int id)
    {
        
        BO.ProductItem pi = new BO.ProductItem();
        if (id > 0)
        {
            BO.OrderItem p2 = c.Items?.Find(x => x?.ProductID == id) ?? throw new BO.NotExistException("Not exists!");
            DO.Product? p = new DO.Product();
            try
            {
                p = Dal?.Product.GetByCondition(x => x?.ID == id);          
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex);}
            pi = new BO.ProductItem
            {
                ID = (int)p?.ID!,
                Name = p?.Name,
                Price = (double)p?.Price!,
                Category = (BO.Enums.Category)p?.Category!,
                InStock = (p?.InStock > 0 ? true : false),
                Amount = p2.Amount,
            };
            
            return pi;
        }
        else
            throw new BO.IdSmallThanZeroException("ID small than zero!");
    }
    #endregion

    #region Get List Product ItemInCart
    /// <summary>
    /// return all items in the cart.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetListProductItemInCart(BO.Cart c) 
    {
        var OrderItems = from items in c.Items
                         let p = Dal?.Product.GetByCondition(x => x?.ID == items.ProductID)
                         select new BO.ProductItem()
                         {
                             ID = (int)items?.ID!,
                             Name = items?.Name,
                             Price = (double)items?.Price!,
                             Category = (BO.Enums.Category)p?.Category!,
                             InStock = (p?.InStock > 0 ? true : false),
                             Amount = items.Amount,                       
                         };
        return (IEnumerable<BO.ProductItem>)OrderItems;
    }
    #endregion

    #region Add product
    /// <summary>
    /// Adds a product to the data layer if the data is correct
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BO.IdSmallThanZeroException"></exception>
    /// <exception cref="BO.NameIsEmptyException"></exception>
    /// <exception cref="BO.PriceSmallThanZeroException"></exception>
    /// <exception cref="BO.InStokeSmallThanZeroException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddProduct(BO.Product p)
    {
        if (p.ID < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
           
        if (p.Name == null) throw new BO.NameIsEmptyException("Name is empty!");
           
        if (p.Price < 0) throw new BO.PriceSmallThanZeroException("Price small than zero!");
           
        if (p.InStock < 0)throw new BO.InStokeSmallThanZeroException("InStock small than zero!");
            

        DO.Product p1 = new DO.Product
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (DO.Enums.Category)p.Category!,
            InStock = p.InStock, 

        };
        try
        { 
            Dal?.Product.Add(p1);
        }
        catch (DO.AlreadyExistException ex) { throw new BO.AlreadyExistException("", ex);}
    }
    #endregion

    #region Delete product
    /// <summary>
    /// The function verifies that the product does not appear in any order and then deletes the product
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.NotExistException"></exception>
    /// <exception cref="BO.CanNotDeleteProductException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteProduct(int id)
    {
        if (id < 0) throw new BO.NotExistException("ID small than zero!");
            
        try
        {
            Dal?.Product.GetByCondition(x => x?.ID == id);
        }
        catch (DO.NotExistException ex) { throw new BO.NotExistException("",ex);}

        foreach (DO.OrderItem? p in Dal?.OrderItem.GetAll() ?? throw new NullReferenceException())
        {
            if (p?.ProductID == id) throw new BO.CanNotDeleteProductException("Can't delete product!");
    
        }
        Dal.Product.Delete(id);
    }
    #endregion

    #region Update product
    /// <summary>
    /// Checks if the data is correct and if so you update the product
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="BO.IdSmallThanZeroException"></exception>
    /// <exception cref="BO.NameIsEmptyException"></exception>
    /// <exception cref="BO.PriceSmallThanZeroException"></exception>
    /// <exception cref="BO.InStokeSmallThanZeroException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateProduct(BO.Product p)
    {
        if (p.ID < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");

        if (p.Name == null) throw new BO.NameIsEmptyException("Name is empty!");
            
        if (p.Price < 0) throw new BO.PriceSmallThanZeroException("Price small than zero!");
            
        if (p.InStock < 0) throw new BO.InStokeSmallThanZeroException("InStock small than zero!");
            

        DO.Product p1 = new DO.Product
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (DO.Enums.Category)p.Category!,
            InStock = p.InStock,

        };
        try
        {
            Dal?.Product.Update(p1);
        }
        catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
    }
    #endregion

    #region Get list of product by condition
    /// <summary>
    /// Helper function for the display layer pl
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList?> GetListByCondition(Func<BO.ProductForList?, bool>? filter)
    {
        var p = from item in GetProductList()
                where (filter!(item))
                select item;
        return p!;
    }
    #endregion

    #region Get list of productItem
    /// <summary>
    /// Return Get List Of ProductItem
    /// </summary>
    /// <param name="cart"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem?> GetListOfProductItem(BO.Cart cart)
    {
        var ListofProductItem = from item in Dal?.Product.GetAll()
                                //let amount = (cart.Items == null ? 0 : cart.Items.Find(x => x?.ProductID == item?.ID)!.Amount)
                                select new BO.ProductItem
                                {
                                    ID = (int)item?.ID!,
                                    Name = item?.Name,
                                    Price = (double)item?.Price!,
                                    Category = (BO.Enums.Category)item?.Category!,
                                    InStock = (item?.InStock > 0 ? true : false),
                                    Amount = cart.Items != null && cart.Items.FirstOrDefault(x =>x?.ProductID == item?.ID) != null ? cart.Items.FirstOrDefault(x => x?.ProductID == item?.ID)!.Amount : 0,
                                };
        
        return ListofProductItem;
    }
    #endregion

    #region Get list productI grouping
    /// <summary>
    /// return list productI grouping
    /// </summary>
    /// <param name="list"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem?> GetListProductIGrouping(IEnumerable<BO.ProductItem?> list,BO.Enums.Category category)
    {
        
        var group = from item in list
                    group item by item.Category into g
                    select g;
        foreach (var g in group)
        {
            if (g.Key == category)
            {
                list = g; 

            }
        }
        return list;
    }
    #endregion

}
