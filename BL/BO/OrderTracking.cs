
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<Tuple<(DateTime, string)>> Pair = new List<Tuple<(DateTime, string)>>();
    
    public override string ToString() => $@"
                ID:{ID}
                Status:{Status}
                Package progress:{Pair}
                                              ";
}