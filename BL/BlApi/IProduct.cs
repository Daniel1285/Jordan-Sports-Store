using BO;
namespace BlApi
{
    public interface IProduct
    {
        /// <summary>
        /// Product list request (for the manager screen and for the buyer's catalog screen.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList?> GetProductList();

        /// <summary>
        /// Product details request for manager.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id);

        /// <summary>
        /// Product details request (for buyer screen - from the catalog) by shopping basket and product ID number.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductItem GetProduct(Cart c, int id);

        /// <summary>
        ///  Adding a product (for admin screen).
        /// </summary>
        /// <param name="p"></param>
        public void AddProduct(Product p);


        /// <summary>
        /// Product deletion (for admin screen).
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id);

        /// <summary>
        /// Update product data (for admin screen).
        /// </summary>
        /// <param name="p"></param>
        public void UpdateProduct(Product p);

        /// <summary>
        /// Get list of product by condition check. 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<Product?> GetListByCondition(Func<DO.Product?, bool>? filter);



    }
}
