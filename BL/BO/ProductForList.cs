

namespace BO;

public class ProductForList
{
    public int ID { get; set; } 
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Enums.Category Category { get; set; }
    public override string ToString() => $@" 
                 ID:{ID}
                 Name:{Name}
                 Price:{Price}
                 Category:{Category}

                                             ";


}
