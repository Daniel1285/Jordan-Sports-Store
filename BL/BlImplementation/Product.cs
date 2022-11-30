using BlApi;
using DalApi;
using Dal;


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
            catch (Exception) { }
      
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
                catch (BO.NotExistException ex) { Console.WriteLine(ex); }
                return p1;
            }

            else
                throw new BO.IdSmallThanZeroException("ID small than zero");

        }
        public BO.ProductItem GetProduct(BO.Cart c, int id)
        {
            BO.ProductItem pi = null;
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
                        //Amount = c.Items.Find(x => x.ID == id).Amount,
                    };
                }
                catch (BO.NotExistException ex) { Console.WriteLine(ex); }
                return pi;
            }
            else
                throw new BO.IdSmallThanZeroException("ID small than zero");
        }
        public void AddProduct(BO.Product p)
        {
            if (p.ID < 0) throw new BO.NotExistException("ID small than zero");
               
            if (p.Name == null) throw new BO.NameIsEmptyException("############################");
               
            if (p.Price < 0) throw new BO.PriceSmallThanZeroException("############################");
               
            if (p.InStock < 0)throw new BO.InStokeSmallThanZeroException("############################");
                

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
                Dal.Product.GetByID(p.ID);
            }
            catch (DO.AlreadyExistException ex) { Console.WriteLine(ex);}
            Dal.Product.Add(p1);
        }
        public void DeleteProduct(int id)
        {
            if (id < 0) throw new BO.NotExistException("ID small than zero");
                
            try
            {
                Dal.Product.GetByID(id);
            }
            catch (DO.NotExistException ex) { Console.WriteLine(ex);}

            foreach (var p in Dal.OrderItem.GetAll())
            {
                if (p.ProductID == id) throw new BO.CanNotDeleteProductException("############");
        
            }
            Dal.Product.Delete(id);
   
        }
        public void UpdateProduct(BO.Product p)
        {
            if (p.ID < 0) throw new BO.NotExistException("ID small than zero");

            if (p.Name == null) throw new BO.NameIsEmptyException("############################");
                
            if (p.Price < 0) throw new BO.PriceSmallThanZeroException("############################");
                
            if (p.InStock < 0) throw new BO.InStokeSmallThanZeroException("############################");
                

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
            catch (DO.NotExistException ex) { Console.WriteLine(ex); }
        }

    }
}
