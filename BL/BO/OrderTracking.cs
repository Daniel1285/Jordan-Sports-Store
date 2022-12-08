
using System.Collections.Generic;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<Tuple<DateTime, Enums.OrderStatus>?>? Pair { get; set; } 

    public override string ToString()
    {
        Console.WriteLine($"\t\tID: {ID} \n" +
                          $" \t\tStatus: {Status}");

        foreach (var tuple in Pair!)
        {
            Console.WriteLine("\t\t{0} \n \t\t{1} ", tuple?.Item1, tuple?.Item2);
        }
        return null!;

    }                                       
}