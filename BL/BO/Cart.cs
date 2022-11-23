﻿namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }   
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }    
        public OrderItem? Items { get; set; }  
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
             Name of customer:{CustomerName}
             Email of customer:{CustomerEmail}
             Address of custumer:{CustomerAddress}
             Items :{Items}
             Total price:{TotalPrice}
                                            ";

        
    }
}
