using BlApi;


namespace BlImplementation
{
    internal class Cart: ICart
    {

        public Cart AddProdctToCatrt(Cart cart, int id)
        {
            return cart;
        }

        public Cart UpdateAmountOfProduct(Cart c, int id, int amount)
        {
            return c;
        }

        public void ConfirmOrder(Cart c)
        {

        }

    }
}
