
namespace DO;

/// <summary>
/// Structure for Customer details.
/// </summary>
public struct Order
{
    /// <summary>
    /// Identification number.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// The name of the ordering customer.
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Customer's email address.
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Customer's address.
    /// </summary>
    public string CustomerAdress { get; set; }

    /// <summary>
    /// Order creation date.
    /// </summary>
    public DateTime OrderDate  { get; set; }

    /// <summary>
    /// delivery date.
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// Date of delivery.
    /// </summary>
    public DateTime DeliveryrDate { get; set; }

    /// <summary>
    /// The object print function.
    /// </summary>
    /// <returns></returns>
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

