
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<(DateTime, string)> Pair = new List<(DateTime, string)>();
    
    public override string ToString() => $@"
                ID:{ID}
                Status:{Status}
                Package progress:{Pair}
                                              ";
}