using BlApi;

namespace BlImplementation
{
    internal class Cart : ICart
    {

        public BO.Cart AddProdctToCatrt(BO.Cart cart, int id)
        {
            return cart;
        }

        public BO.Cart UpdateAmountOfProduct(BO.Cart c, int id, int amount)
        {
            return c;
        }

        public void ConfirmOrder(BO.Cart c)
        {

        }

    }
}
