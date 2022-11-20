
using DO;

namespace DalApi
{
    internal interface IDal
    {
        public Product Product { get; }

        public OrderItem OrderItem { get; }
        public Order Order { get; } 
        
    }
}
