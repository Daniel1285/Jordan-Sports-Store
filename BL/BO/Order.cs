

namespace BO;

public class Order
{
    public int ID { get; set; } 
    public string ? CustomerName { get; set; }  
    public string? CustomerEmail { get; set; }  
    public string? CustomerAddress { get; set; }    
    public DateTime? OrderDate { get; set; } 
    public Enums.OrderStatus Status { get; set; }   
    public DateTime? ShipDate    { get; set; }
    public DateTime? DeliveryDate { get; set; }  
    public List<OrderItem?>?Items { get; set; }    
    public Double? TotalPrice { get; set; }
    public override string ToString()
    {

        Console.WriteLine($"              Order ID = {ID} \n" +
             $"              Name of customer:{CustomerName}\n" +
             $"              Email of customer:{CustomerEmail}\n" +
             $"              Address of custumer:{CustomerAddress} \n" +
             $"              Order date :{OrderDate}  \n" +
             $"              Status: {Status}\n" +
             $"              Ship date: {ShipDate}  \n" +
             $"              Delivery date:{DeliveryDate}  \n");

        foreach (var item in Items)
        {
            Console.WriteLine(item);
        }
        Console.Write("     Total price: ");
        return TotalPrice.ToString();
    }

}                                       

