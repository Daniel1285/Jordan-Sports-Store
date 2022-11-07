
namespace DO;

/// <summary>
/// Structure for Product definition
/// </summary>
public struct Product 
 {
     /// <summary>
     /// Product ID number.
     /// </summary>
     public int ID { get; set; }

     /// <summary>
     /// Name of the product.
     /// </summary>
     public string Name { get; set; } 

     /// <summary>
     /// price of the product.
     /// </summary>
     public double Price { get; set; } 

     /// <summary>
     /// Category of product.
     /// </summary>
     public  Enums.Category Category { get; set; } 

     /// <summary>
     /// Product quantity in stock.
     /// </summary>
     public int InStock { get; set; }

     /// <summary>
     /// The object print function.
     /// </summary>
     /// <returns> get of all fields </returns>
     public override string ToString() => $@"
            Product ID= {ID}: {Name},
            category - {Category}
            Price: {Price}
            Amount in stock: {InStock}
                                        ";
 }

    