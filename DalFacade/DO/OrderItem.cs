
namespace DO;

/// <summary>
/// Structure for Product order items
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Product ID number
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// Order ID number
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Price per unit
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Quantity of order units of the product
    /// </summary>
    public int Amount { get; set; }


    /// <summary>
    /// The object print function
    /// </summary>
    /// <returns> get of all fields </returns>
    public override string ToString() => $@"
          Product ID= {ProductID}
          Order ID - {OrderID}
          Price: {Price}
          Amount order - {Amount} ";
}





