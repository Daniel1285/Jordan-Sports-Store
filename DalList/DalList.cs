
using DalApi;

namespace Dal;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();

    private DalList() 
    { 
        Order = new DalOrder();
        Product= new DalProduct();
        OrderItem = new DalOrderItem(); 
    }
    public IProduct Product { get; }
    public IOrder Order { get; } 
    public IOrderItem OrderItem { get; }
}
