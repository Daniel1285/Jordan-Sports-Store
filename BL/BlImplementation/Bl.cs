
using BlApi;


namespace BlImplementation
{
    sealed internal class Bl: IBl
    {
        public ICart Cart => new Cart();
        public IOrder Order => new Order();
        public IProduct Product => new Product();   


    }
}
