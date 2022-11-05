
namespace DO;

/// <summary>
/// Order details
/// </summary>
public struct Order
{
    /// <summary>
    /// Identification number
    /// </summary>
    private int ID { get; set; }
    /// <summary>
    /// The name of the ordering customer
    /// </summary>
    private string CustomerName { get; set; }
    /// <summary>
    /// Customer's email address
    /// </summary>
    private string CustomerEmail { get; set; }
    /// <summary>
    /// Customer's address
    /// </summary>
    private string CustomerAdress { get; set; }
    /// <summary>
    /// Order creation date
    /// </summary>
    private DateTime OrderDate  { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    private DateTime ShipDate { get; set; }
    /// <summary>
    /// Date of delivery
    /// </summary>
    private DateTime DeliveryrDate { get; set; }
    public override string ToString() => $@"
        Product ID={ID}
        Customer Name:{CustomerName}
        Customer Email:{CustomerEmail}
        Customer Adress:{CustomerAdress}
        Order Date:{OrderDate}
        Ship Date:{ShipDate}
        DeliveryrDate:{DeliveryrDate}
        ";
   
    

}

