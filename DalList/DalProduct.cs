
using DO;
using DalApi;
namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// Add a product to array "MyProducts" in DataSource and increases the size of the array "SizeOfProducts" by one, and check if ID product alrady exsistn. 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int Add(Product p)
    {
        foreach (Product? s  in DataSource.MyProducts)
        {
            if (p.ID == s?.ID )
            {
                throw new AlreadyExistException("the product alrady exist!");
            }
        }
        DataSource.MyProducts.Add(p);
        return p.ID;
    }

    /// <summary>
    /// Deleteing a product from the array "MyProducts".
    /// </summary>
    /// <param name="ID"></param>
    public void Delete(int ID)
    {
        var product1 = (from item in DataSource.MyProducts
                        where (item?.ID == ID)
                        select item).FirstOrDefault();
        if (product1 != null)
        { 
            DataSource.MyProducts.Remove(product1);
            return;
        }
        throw new NotExistException("Not found Product to delete");
           

    }


    /// <summary>
    /// Updates a product by overwriting an existing product.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product p)
    {
        Delete(p.ID);
        Add(p); 
    }



    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetByCondition(Func<Product?, bool>? filter)
    {
        var product1 = (from item in DataSource.MyProducts
                       where(filter!(item))
                       select item).FirstOrDefault();
        if(product1 != null)
        {
            return (Product)product1;
        }
        throw new NotExistException("NOT exists!");
    }

    /// <summary>
    /// Returns All products in the array.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
       
        if (filter == null)
        {
            var list = from item in DataSource.MyProducts
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.MyProducts
                       where (filter(item))
                       select item;
            return list;
        }
        
    }
            
 }



