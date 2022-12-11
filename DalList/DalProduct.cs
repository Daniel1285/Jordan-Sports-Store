
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
        bool flag = false;
        for (int i = 0; i < DataSource.MyProducts.Count; i++)
        {
            if (ID == DataSource.MyProducts[i]?.ID)
            {
                DataSource.MyProducts.RemoveAt(i);  
                flag = true;
                break;
            }
        }
        if (flag == false)
            throw new NotExistException("Not found Product to delete");

    }


    /// <summary>
    /// Updates a product by overwriting an existing product.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product p)
    {
        
        for (int i = 0; i < DataSource.MyProducts.Count; i++)
        {
            if (p.ID == DataSource.MyProducts[i]?.ID)
            {
                DataSource.MyProducts[i] = p;

                return;
            }
        }

        throw new NotExistException("Not found Product to Update");
    }



    /// <summary>
    /// Receives a function for testing (for example, ID resonance) and returns an object according to this
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetByCondition(Func<Product?, bool>? filter)
    {
        foreach (Product? p in DataSource.MyProducts)
        {
            if (filter!(p))
            {
                return (Product)p!;
            }
        }
        throw new DO.NotExistException("NOT exists!");
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



