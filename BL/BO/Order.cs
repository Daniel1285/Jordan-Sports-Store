

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
    public List<OrderItem>Items { get; set; }    
    public Double? TotalPrice { get; set; }
    public override string ToString()
    {

        Console.WriteLine($"Order ID = {ID}" +
             $"Name of customer:{CustomerName}" +
             $"Email of customer:{CustomerEmail}" +
             $"Address of custumer:{CustomerAddress}" +
             $"Order date :{OrderDate}" +
             $"Status: {Status}" +
             $"Ship date: {ShipDate}" +
             $"Delivery date:{DeliveryDate}");

        foreach (var item in Items)
        {
            Console.WriteLine($"{item}" + item);
        }

        return TotalPrice.ToString();
    }

}                                       

