

using DO;

namespace Dal;

public class DalProduct
{
    /// <summary>
    /// Add a product to array "MyProducts" in DataSource and increases the size of the array "SizeOfProducts" by one.
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int AddNewProduct(Product p)
    {
        DataSource.MyProducts[DataSource.Config.SizeOfProducts++] = p;
        return p.ID;
    }

    /// <summary>
    /// Deleteing a product from the array "MyProducts".
    /// </summary>
    /// <param name="ID"></param>
    public void DeleteProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.SizeOfProducts; i++)
        {
            if (ID == DataSource.MyProducts[i].ID)
            {
                DataSource.MyProducts[i].ID = 0;
            }
        }
    }


    /// <summary>
    /// Updates a product by overwriting an existing product.
    /// </summary>
    /// <param name="p"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateProduct(Product p)
    {
        for (int i = 0; i < DataSource.Config.SizeOfProducts; i++)
        {
            if (p.ID == DataSource.MyProducts[i].ID)
            {
                DataSource.MyProducts[i] = p;
                return;
            }
        }

        throw new Exception("Not found Product to Update");
    }


    /// <summary>
    /// Returns a product by ID number.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetProduct(int ID)
    {
        foreach (Product p in DataSource.MyProducts)
        {
            if (ID == p.ID)
            {
                p.ToString();
            }
            return p;
        }
        throw new Exception("Product not found");
    }

}

