using BlApi;
using DalApi;
using Dal;


namespace BlImplementation
{
    /// <summary>
    /// Implementation of product functions of BO->product
    /// </summary>
    internal class Product : BlApi.IProduct
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// We will ask DO for a list of products and build a new list of ProductForList  
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetProductList()
        {
            List<DO.Product> products = new List<DO.Product>();
            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
            products = Dal.Product.GetAll().ToList();
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
        /// <summary>
        /// The function builds a new product BO by the received data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.Product GetProduct(int id)
        {
            BO.Product p1 = null;
            if (id > 0)
            {
                try
                {
                    DO.Product p = new DO.Product();
                    p = Dal.Product.GetByID(id);

                    p1 = new BO.Product
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Price = p.Price,
                        Category = (BO.Enums.Category)p.Category,
                        InStock = p.InStock,
                    };
                   
                }
                catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex);}
                return p1;
            }

            else
                throw new BO.IdSmallThanZeroException("ID small than zero!");

        }
        /// <summary>
        /// The function builds a new product item BO by the received data
        /// </summary>
        /// <param name="c"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.ProductItem GetProduct(BO.Cart c, int id)
        {
            
            BO.ProductItem pi = new BO.ProductItem();
            if (id > 0)
            {
                try
                {
                    DO.Product p = new DO.Product();
                    p = Dal.Product.GetByID(id);
                    pi = new BO.ProductItem
                    {
                        ID = p.ID,
                        Name = p.Name,
                        Price = p.Price,
                        Category = (BO.Enums.Category)p.Category,
                        InStock = (p.InStock > 0 ? true : false),
                        Amount = c.Items.Find(x => x.ProductID == id).Amount,
                
                    };
                       
                }
                catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
                return pi;
            }
            else
                throw new BO.IdSmallThanZeroException("ID small than zero!");
        }
        /// <summary>
        /// Adds a product to the data layer if the data is correct
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        /// <exception cref="BO.NameIsEmptyException"></exception>
        /// <exception cref="BO.PriceSmallThanZeroException"></exception>
        /// <exception cref="BO.InStokeSmallThanZeroException"></exception>
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
                Category = (DO.Enums.Category)p.Category,
                InStock = p.InStock, 

            };
            try
            {
                Dal.Product.Add(p1);
            }
            catch (DO.AlreadyExistException ex) { throw new BO.AlreadyExistException("", ex);}
        }
        /// <summary>
        /// The function verifies that the product does not appear in any order and then deletes the product
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BO.NotExistException"></exception>
        /// <exception cref="BO.CanNotDeleteProductException"></exception>
        public void DeleteProduct(int id)
        {
            if (id < 0) throw new BO.NotExistException("ID small than zero!");
                
            try
            {
                Dal.Product.GetByID(id);
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("",ex);}

            foreach (var p in Dal.OrderItem.GetAll())
            {
                if (p.ProductID == id) throw new BO.CanNotDeleteProductException("Can't deleta product!");
        
            }
            Dal.Product.Delete(id);
   
        }
        /// <summary>
        /// Checks if the data is correct and if so you update the product
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        /// <exception cref="BO.NameIsEmptyException"></exception>
        /// <exception cref="BO.PriceSmallThanZeroException"></exception>
        /// <exception cref="BO.InStokeSmallThanZeroException"></exception>
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
                Category = (DO.Enums.Category)p.Category,
                InStock = p.InStock,

            };
            try
            {
                Dal.Product.Update(p1);
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
        }

    }
}
