
using DO;
using System.Numerics;
using System.Xml.Linq;

namespace Dal;
/// <summary>
/// 
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// data about the entity
    /// </summary>
    internal static class Config
    {
        // Size arrays in real time
        internal static int SizeOfProducts = 0;
        internal static int SizeOfOrder = 0;
        internal static int SizeOfOrderItem = 0;

        private static int IdForOrder { get => IdForOrder++; set => IdForOrder = 1;} // Automatic ID number for order
        private static int IdForOrderItem { get => IdForOrderItem++; set => IdForOrderItem = 1;}   // Automatic ID number for order item
    }

    private static readonly Random R = new Random(); // Random number generation

    internal static Product[] MyProducts = new Product[50];
    internal static Order[] MyOrder = new Order[100];
    internal static OrderItem[] MyOrderItem = new OrderItem[200];

    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="P"></param>
    private static void AddProduct(Product[] p) 
    {
        int size = 10;
        string[] namePprodcut = { "shoes - Air Jordan 1 Zoom CMFT", "shoes - Air Jordan 1 Element", "short - Air Jordan x Titan",};
        int[] priceOfProduct = { 550, 400, 250 };
        for (int i = 0; i < size; i++)
        {
            Product newProduct = new Product(); 
            newProduct.ID = R.Next(100000,999999);
            newProduct.Name = namePprodcut[i];
            newProduct.Price = priceOfProduct[i];
            newProduct.InStock = R.Next(0,30);
            Enums.Category num = (Enums.Category)i;
            newProduct.Category = num;
            
        }
    }

    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="O"></param>
    private static void AddOrder(Order [] O)
    {
        int size = 20;
        DateTime OrderDate = DateTime.Now - new TimeSpan(365,0,0,0); 
        for(int i = 0; i < size; i++)
        {
            Order newOrder = new Order();

        }
      
    }

    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="O_i"></param>
    private static void Add_OrderItem(OrderItem[] O_i)
    {

        
    }

    /// <summary>
    /// 
    /// </summary>
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// A method that calls the functions of adding objects to arrays of entities
    /// </summary>
    private static void s_Initialize()
    {
        AddProduct(MyProducts);
        Add_OrderItem(MyOrderItem);
        AddOrder(MyOrder);
    }









}
