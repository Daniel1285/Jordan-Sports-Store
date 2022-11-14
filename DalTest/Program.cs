using Dal;
using DO;
namespace DalTest
{
    internal class Program
    {
        private static DalProduct testProduct = new DalProduct();
        private static DalOrderItem testOrderItem = new DalOrderItem();  
        private static DalOrder testOrder = new DalOrder();

        
        static void Main(string[] args)
        {
            
            int choice;
            do
            {
                Console.WriteLine("Enter your choice:\n 0. Exit. \n 1. Product. \n 2. OrderItem. \n 3. Order");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case (int)Enums.StartChoose.EXIT:
                        Console.WriteLine("Have a good day");
                        break;  

                    case (int)Enums.StartChoose.PRODUCT:
                        choiceProduct();
                        break;

                    case (int)Enums.StartChoose.ORDERITEM:
                        choiceOrderItem();
                        break;

                    case (int)Enums.StartChoose.ORDER:
                        choiceOrder();
                        break;

                    default:
                        break;
                }
            } while (choice != 0);
            Console.WriteLine();
        }

//----------------------------------------------------------->>> MAIN'S FUNCTIONS <<<------------------------------------------------------------

        /// <summary>
        /// Feasibility for "product" entity.
        /// </summary>
        public static void choiceProduct()
        {
            Console.WriteLine("Please enter your choice: \n a. add product. \n b. Product display option by ID. \n c. Product list view option. \n d. Update product data. \n e. Delete product.");
            string choise = Console.ReadLine();
            Product p = new Product();
            switch (choise)
            {
                case "a": 

                    Console.WriteLine("Please enter a product ID number.");
                    p.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter Product Name Product ID.");
                    p.Name = Console.ReadLine();

                    Console.WriteLine("Please enter a product price.");
                    p.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise2 = int.Parse(Console.ReadLine());
                    p.Category = (Enums.Category)choise2;

                    Console.WriteLine("Please enter the quantity of the product in stock.");
                    p.InStock = int.Parse(Console.ReadLine());

                    try
                    {
                        testProduct.AddNewProduct(p);
                    }
                    catch (Exception str) { Console.WriteLine(str); }
                    
                    break;

                case "b":

                    Console.WriteLine("Enter the Product ID.");
                    int ID = int.Parse(Console.ReadLine());
                    try
                    {
                        testProduct.GetProduct(ID);
                    }
                    catch (Exception str) { Console.WriteLine(str); }
                    
                    Console.WriteLine(testProduct.GetProduct(ID));
                    break;

                case "c":
                    Product[] products = testProduct.GetAllProducts();
                    for (int i = 0; i < products.Length; i++)
                    {
                        Console.WriteLine(testProduct.GetProduct(products[i].ID));
                    }
                    
                    break;

                case "d":

                    p = new Product();
                    Console.WriteLine("Enter the ID number of the product you want to update:");
                    int ID2 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testProduct.GetProduct(ID2));
                    }
                    catch (Exception str) { Console.WriteLine(str); }
                    
                    Console.WriteLine("Please enter the product to update:");

                    Console.WriteLine("Please enter a product ID number:");
                    p.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter Product Name Product ID:");
                    p.Name = Console.ReadLine();

                    Console.WriteLine("Please enter a product price:");
                    p.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose. 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise3 = int.Parse(Console.ReadLine());
                    p.Category = (Enums.Category)choise3;

                    Console.WriteLine("Please enter the quantity of the product in stock:");
                    p.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        testProduct.UpdateProduct(ref p);
                    }
                    catch (Exception str) { Console.WriteLine(str);}
      
                    break;


                case "e":

                    Console.WriteLine("Enter the Product ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testProduct.DeleteProduct(IDd);
                    }
                    catch (Exception str) { Console.WriteLine(str); }

                    break;

                default:
                    break;

            }
        }

 //------------------------------------------------->>>>> End of start with Product <<<<<--------------------------------------------------

        /// <summary>
        /// Feasibility for "Order item" entity.
        /// </summary>
        public static void choiceOrderItem()
        {
            Console.WriteLine("Please enter your choice: \n a. add Order Item. \n b. Order Item display option by ID. \n c. Product list view option. \n d. Update Order item data. \n e. Delete order item. \n f. return order item in the order. ");
            string choise = Console.ReadLine();
            OrderItem item = new OrderItem();
            switch (choise)
            {
                case "a":

                    Console.WriteLine("Please enter a Order item ID number:");
                    item.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter the Product ID:");
                    item.ProductID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter a Order ID:");
                    item.OrderID = int.Parse(Console.ReadLine());

                    Console.WriteLine("please enter the price:");
                    item.Price = int.Parse(Console.ReadLine()); 

                    Console.WriteLine("Please enter the amount of order item:");
                    item.Amount = int.Parse(Console.ReadLine());

                    try
                    {
                        testOrderItem.AddNewOrderItem(item);
                    }
                    catch (Exception str) { Console.WriteLine(str);}    
                    

                    break;

                case "b":

                    Console.WriteLine("Enter the Product ID:");
                    int IDp = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the Order ID:");
                    int IDo = int.Parse(Console.ReadLine());

                    try
                    {
                        testOrderItem.GetOrderItem(IDp, IDo);
                    }
                    catch (Exception str) { Console.WriteLine(str); }
     
                    
                    Console.WriteLine(testOrderItem.GetOrderItem(IDp, IDo)); 

                    break;

                case "c":

                    try
                    {
                        OrderItem[] items = testOrderItem.GetAllOrdersItem();
                        for (int i = 0; i < items.Length; i++)
                        {
                            Console.WriteLine(testOrderItem.GetOrderItem(items[i].ProductID, items[i].OrderID));
                        }
                    }
                    catch (Exception str) { Console.WriteLine(str); }
  
                    
                    break;

                case "d":

                    item = new OrderItem();
                    Console.WriteLine("For update please enter a Order item ID number:");
                    int ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("For update please enter the Product ID:");
                    int ProductID = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testOrderItem.GetOrderItem(ID, ProductID));
                    }
                    catch (Exception str) { Console.WriteLine(str); }   
       
                    
                    Console.WriteLine("Please enter the details of the Order item you want to update:");
                  
                    Console.WriteLine("Please enter a Order item ID number:");
                    item.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter the Product ID:");
                    item.ProductID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter a Order ID:");
                    item.OrderID = int.Parse(Console.ReadLine());

                    Console.WriteLine("please enter the price:");
                    item.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter the amount of order item:");
                    item.Amount = int.Parse(Console.ReadLine());
                    try
                    {
                        testOrderItem.UpdateOrderItem(ref item);
                    }
                    catch (Exception str) { Console.WriteLine(str); }   

                    break;

                case "e":

                    Console.WriteLine("Enter the Order item ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testOrderItem.DeleteOrderItem(IDd);
                    }
                    catch (Exception str) { Console.WriteLine(str); }   
        
                    break;

                case "f":
                    Console.WriteLine("Enter ID of order:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        testOrderItem.GetOrdersItem(ID);
                    }
                    catch (Exception str) { Console.WriteLine(str); }
    
                    
                    Console.WriteLine(testOrderItem.GetOrdersItem(ID));
                    break;

                default:
                    break;

            }
        }

        //------------------------------------------------->>>>> End of start with Order item <<<<<--------------------------------------------------

        /// <summary>
        /// Feasibility for "Order" entity.
        /// </summary>
        public static void choiceOrder()
        {
            Console.WriteLine("Please enter your choice: \n a. add Order. \n b. Order display option by ID. \n c. Order list view option. \n d. Update Order data. \n e. Delete Order.");
            string choise = Console.ReadLine();
            Order o = new Order();  

            switch (choise)
            {
                case "a":

                    Console.WriteLine("Please enter order ID number:");
                    o.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter the CustomerName:");
                    o.CustomerName = Console.ReadLine();

                    Console.WriteLine("Please enter the CustomerEmail:");
                    o.CustomerEmail = Console.ReadLine();

                    Console.WriteLine("Please enter the CustomerAdress:");
                    o.CustomerAdress = Console.ReadLine();

                    try
                    {
                        testOrder.AddNewOrder(o);
                    }
                    catch (Exception str) { Console.WriteLine(str); }   

                    break;
                case "b":

                    Console.WriteLine("Enter the oredr ID:");
                    int IDo = int.Parse(Console.ReadLine());

                    try
                    {
                        testOrder.GetOrder(IDo);
                    }
                    catch (Exception str) { Console.WriteLine(str); }   
 
                    
                    Console.WriteLine(testOrder.GetOrder(IDo));

                    break;

                case "c":
                    try
                    {
                        Order[] orders = testOrder.GetAllOrders();
                        for (int i = 0; i < orders.Length; i++)
                        {
                            Console.WriteLine(testOrder.GetOrder(orders[i].ID));
                        }
                    }
                    catch (Exception str) { Console.WriteLine(str); }
              
                    break;
                case "d":

                    o = new Order();
                    Console.WriteLine("For update please enter a Order ID number:");
                    int ID = int.Parse(Console.ReadLine());

                    try
                    {
                        Console.WriteLine(testOrder.GetOrder(ID));
                    }
                    catch (Exception str) { Console.WriteLine(str); }   
      
                    
                    Console.WriteLine("Please enter the details of the Order  you want to update:");

                    Console.WriteLine("Please enter order ID number:");
                    o.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter the CustomerName:");
                    o.CustomerName = Console.ReadLine();

                    Console.WriteLine("Please enter the CustomerEmail:");
                    o.CustomerEmail = Console.ReadLine();

                    Console.WriteLine("Please enter the CustomerAdress:");
                    o.CustomerAdress = Console.ReadLine();

                    try
                    {
                        testOrder.UpdateOrder(ref o);
                    }
                    catch (Exception str) { Console.WriteLine(str); }   
          
                    break;
                case "e":

                    Console.WriteLine("Enter the Order ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testOrder.DeleteOrder(IDd);
                    }
                    catch (Exception str) { Console.WriteLine(str); }

                    break;

                default:
                    break;

            }
        }
    }

    //------------------------------------------------->>>>> End of start with Order <<<<<--------------------------------------------------

}