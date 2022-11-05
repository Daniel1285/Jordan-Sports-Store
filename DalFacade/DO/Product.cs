
using System.ComponentModel;

namespace DO;
    /// <summary>
    /// Structure for Product definition
    /// </summary>
    public struct Product
    {
        private int Id { get; set; }
        private string Name { get; set; } 
        private double Price { get; set; } 
        private CategoryAttribute Category { get; set; }    
        private int InStoke { get; set; }   


        
    }

    