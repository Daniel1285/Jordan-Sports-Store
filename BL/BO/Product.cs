

namespace BO;

public class Product
{
    public int ID { get; set; }
    public string? Name { get; set; }  
    public double Price { get; set; }   
    public Enums.Category? Category { get; set; }    
    public int InStock { get; set; }
    public override string ToString() => $@" 
            Product ID= {ID}: 
            Name of product: {Name},
            category - {Category}
            Price: {Price}
            Amount in stock: {InStock}
                                         ";
}
