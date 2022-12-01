using Dal;
using DalApi;
using DO;
namespace DalTest
{
    internal class Program
    {
      
        private static IDal testMain = new DalList();
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("\nEnter your choice:\n 0. Exit. \n 1. Product. \n 2. OrderItem. \n 3. Order");
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

                    Console.WriteLine("Please enter Product Name:.");
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
                        testMain.Product.Add(p);    
                    }
                    catch (AlreadyExistException str) { Console.WriteLine(str); }
                    
                    break;

                case "b":

                    Console.WriteLine("Enter the Product ID.");
                    int ID = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testMain.Product.GetByID(ID));  
                    }
                    catch (NotExistException str) { Console.WriteLine(str);}
                    
                    
                    break;

                case "c":
                    try
                    {
                       
                        IEnumerable<Product>? products = testMain.Product.GetAll();
                        foreach (var product in products)
                        {
                            Console.WriteLine(testMain.Product.GetByID(product.ID));
                        }
                        
                    }

                    catch (NotExistException str) { Console.WriteLine(str); }

                    break;

                case "d":

                    p = new Product();
                    Console.WriteLine("Enter the ID number of the product you want to update:");
                    int ID2 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testMain.Product.GetByID(ID2));
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }

                    
                    Console.WriteLine("Please enter the product to update:");

                    Console.WriteLine("Please enter a product ID number:");
                    p.ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please enter Product Name Product ID:");
                    p.Name = Console.ReadLine();

                    Console.WriteLine("Please enter a product price:");
                    p.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise3 = int.Parse(Console.ReadLine());
                    p.Category = (Enums.Category)choise3;

                    Console.WriteLine("Please enter the quantity of the product in stock:");
                    p.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Product.Update(p);
                    }
                    catch (NotExistException str) { Console.WriteLine(str);}
      
                    break;


                case "e":

                    Console.WriteLine("Enter the Product ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Product.Delete(IDd);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }

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
            Console.WriteLine("Please enter your choice: \n a. add Order Item. \n b. Order Item display option by ID. \n c. Product list view option. \n d. Update Order item data. \n e. Delete order item. \n f. show orderItem ");
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

                    testMain.OrderItem.Add(item);
                     
                    

                    break;

                case "b":

                    Console.WriteLine("Enter the Product ID:");
                    int IDp = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the Order ID:");
                    int IDo = int.Parse(Console.ReadLine());

                    try
                    {
                        testMain.OrderItem.GetByID(IDo);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }
     
                    Console.WriteLine(testMain.OrderItem.GetByID(IDo)); 

                    break;

                case "c":

                    try
                    {
                        IEnumerable<OrderItem>?items = testMain.OrderItem.GetAll();
                        foreach (var itemOfOrderItem in items)
                        {
                            Console.WriteLine(testMain.OrderItem.GetByID(itemOfOrderItem.OrderID));
                        }    
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }
  
                    
                    break;

                case "d":

                    item = new OrderItem();
                    Console.WriteLine("For update please enter a Order item ID number:");
                    int ID = int.Parse(Console.ReadLine());

                    Console.WriteLine("For update please enter the Product ID:");
                    int ProductID = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testMain.OrderItem.GetByTwoID(ID, ProductID));
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   
       
                    
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
                        testMain.OrderItem.Update(item);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   

                    break;

                case "e":

                    Console.WriteLine("Enter the Order item ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.OrderItem.Delete(IDd);
                        Console.WriteLine("succees");
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   
        
                    break;

                case "f":
                    Console.WriteLine("Enter ID of order:");
                    ID = int.Parse(Console.ReadLine());
                    List<OrderItem> ArrOrders = testMain.OrderItem.GetOrdersItem(ID);

                    try
                    {
                        foreach (var i in ArrOrders)
                        {
                            Console.WriteLine(testMain.OrderItem.GetByID(i.OrderID));
                        }
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }
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

                
                     testMain.Order.Add(o);
                   

                    break;

                case "b":

                    Console.WriteLine("Enter the oredr ID:");
                    int IDo = int.Parse(Console.ReadLine());

                    try
                    {
                        testMain.Order.GetByID(IDo);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   
 
                    
                    Console.WriteLine(testMain.Order.GetByID(IDo));

                    break;

                case "c":
                    try
                    {
                        IEnumerable<Order> orders = testMain.Order.GetAll();
                        foreach (var order in orders)
                        {
                            Console.WriteLine(testMain.Order.GetByID(order.ID));
                        }
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }
              
                    break;
                case "d":

                    o = new Order();
                    Console.WriteLine("For update please enter a Order ID number:");
                    int ID = int.Parse(Console.ReadLine());

                    try
                    {
                        Console.WriteLine(testMain.Order.GetByID(ID));
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   
      
                    
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
                        testMain.Order.Update(o);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }   
          
                    break;
                case "e":

                    Console.WriteLine("Enter the Order ID to delete:");
                    int IDd = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Order.Delete(IDd);
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }

                    break;

                default:
                    break;

            }
        }
    }

    //------------------------------------------------->>>>> End of start with Order <<<<<--------------------------------------------------

}