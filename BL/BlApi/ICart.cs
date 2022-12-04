using BO;

namespace BlApi
{
    public interface ICart
    {
        /// <summary>
        /// Adding a product to the shopping cart (for catalog screen, product details screen).
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Cart AddProdctToCatrt(Cart cart, int id);

        /// <summary>
        /// Updating the quantity of a product in the shopping cart (for the shopping cart screen).
        /// </summary>
        /// <param name="c"></param>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public Cart UpdateAmountOfProduct(Cart c, int id, int amount);

        /// <summary>
        /// Basket confirmation for order \ placing an order (for shopping basket screen or order completion screen)
        /// </summary>
        /// <param name="cart"></param>
        public void ConfirmOrder(Cart c , string name , string email,string adrres);    
    }
}
