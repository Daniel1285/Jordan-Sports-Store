

namespace BO
{
    public class Order
    {
        public int ID { get; set; } 
        public string ? CustomerName { get; set; }  
        public string? CustomerEmail { get; set; }  
        public string? CustomerAddress { get; set; }    
        public DateTime OrderDate { get; set; } 
        public Enums.OrderStatus Status { get; set; }   
        public DateTime PayMentDate { get; set; }   
        public DateTime ShipDate    { get; set; }
        public DateTime DeliveryDate { get; set; }  
        public OrderItem? Items { get; set; }    
        public Double TotalPrice { get; set; }  


    }
}
