
namespace DO;

/// <summary>
/// Structure for Customer details
/// </summary>
public struct Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate  { get; set; }
    public DateTime ShipDate { get; set; } 
    public DateTime DeliveryrDate { get; set; }

}

