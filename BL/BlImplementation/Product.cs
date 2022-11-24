using BlApi;
using DalApi;
using Dal;
using System.Security.Cryptography;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();

        public IEnumerable<BO.ProductForList> GetProductList()
        {
            List<DO.Product> products = new List<DO.Product>();
            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
            try
            {
                products = Dal.Product.GetAll().ToList();
            }
            catch (Exception)
            {

                throw;
            }
            foreach (var item in products)
            {
                productsForList.Add(new BO.ProductForList()
                {
                    ID = item.ID,
                    Name = item.Name,
                    Price = item.Price,
                    Category = (BO.Enums.Category)item.Category

                });
            }

            return productsForList;


        }
        public BO.Product GetProduct(int id)
        {
            
            if (id > 0)
            {
                try
                {
                    DO.Product p = new DO.Product();
                    p = Dal.Product.GetByID(id);

                    BO.Product p1 = new BO.Product
                    {
                        ID = p.ID,
                        Name= p.Name,
                        Price= p.Price,
                        Category= (BO.Enums.Category)p.Category,
                        InStock = p.InStock,
                    };
                    return p1;
                }
                catch (Exception) { throw; }
               
            }

            throw new Exception("sss"); // need to fix it
        }
        public BO.Product GetProduct(BO.Cart c, int id)
        {
            if (id > 0)
            {

            }
        }
        public void AddProduct(BO.Product p)
        {

        }
        public void DeleteProduct(int id)
        {

        }
        public void UpdateProduct(BO.Product p)
        {

        }

    }
}
