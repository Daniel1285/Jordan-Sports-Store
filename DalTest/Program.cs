using Dal;
using DalApi;
using DO;
using System;

namespace DalTest;

internal class Program
{

    private static DalApi.IDal? testMain = DalApi.Factory.Get();
    static void Main(string[] args)
    {
        int choice;
        do
        {
            Console.WriteLine("\nEnter your choice:\n 0. Exit. \n 1. Product. \n 2. OrderItem. \n 3. Order");
            int.TryParse(Console.ReadLine(), out choice);

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
                case 4:
                    XMLTools.SaveListToXMLSerializer(testMain!.Product.GetAll().ToList(), "Product");
                    XMLTools.SaveListToXMLSerializer(testMain.Order.GetAll().ToList(), "Order");
                    XMLTools.SaveListToXMLSerializer(testMain.OrderItem.GetAll().ToList(), "OrderItem");
                    XMLTools.SaveConfigXml("OrderID", testMain.Order.GetAll().Last()?.ID ?? 0);
                    XMLTools.SaveConfigXml("OrderItemID", testMain.OrderItem.GetAll().Last()?.ID ?? 0);
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
        string? choise = Console.ReadLine();
        Product p = new Product();
        int num;
        switch (choise)
        {
            case "a": 

                Console.WriteLine("Please enter a product ID number.");
                int.TryParse(Console.ReadLine(),out num);
                p.ID = num;

                Console.WriteLine("Please enter Product Name:.");
                p.Name = Console.ReadLine();

                Console.WriteLine("Please enter a product price.");
                int.TryParse(Console.ReadLine(), out num);
                p.Price = num;

                Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                int.TryParse(Console.ReadLine(), out num);
                p.Category = (Enums.Category)num;

                Console.WriteLine("Please enter the quantity of the product in stock.");
                int.TryParse(Console.ReadLine(), out num);
                p.InStock = num;

                try
                {
                    testMain?.Product.Add(p);    
                }
                catch (AlreadyExistException str) { Console.WriteLine(str); }
                
                break;

            case "b":

                Console.WriteLine("Enter the Product ID.");
                int.TryParse(Console.ReadLine(), out num);
                try
                {
                    Console.WriteLine(testMain?.Product.GetByCondition(x => x?.ID == num));
                }
                catch (NotExistException str) { Console.WriteLine(str);}
                
                
                break;

            case "c":
                try
                {
                   
                    List<Product?> products = testMain?.Product.GetAll().ToList()?? throw new NullReferenceException();
                    //foreach (DO.Product? product in products)
                    //{
                    //    Console.WriteLine(testMain.Product.GetByCondition(x => x?.ID == product?.ID));
                    //}
                    products.ForEach(p => Console.WriteLine(p));
                }

                catch (NotExistException str) { Console.WriteLine(str); }

                break;

            case "d":
                int ID2;
                p = new Product();
                Console.WriteLine("Enter the ID number of the product you want to update:");
                int.TryParse(Console.ReadLine(),out ID2);
                try
                {
                    Console.WriteLine(testMain?.Product.GetByCondition(x => x?.ID == ID2));
                }
                catch (NotExistException str) { Console.WriteLine(str);break; }

                
                Console.WriteLine("Please enter the product to update:");

                Console.WriteLine("Please enter a product ID number:");
                int.TryParse(Console.ReadLine(), out num);
                p.ID = num;

                Console.WriteLine("Please enter Product Name Product ID:");
                p.Name = Console.ReadLine();

                Console.WriteLine("Please enter a product price:");
                int.TryParse(Console.ReadLine(), out num);
                p.Price = num;
                Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                int.TryParse(Console.ReadLine(), out num);
                p.Category = (Enums.Category)num;

                Console.WriteLine("Please enter the quantity of the product in stock:");
                int.TryParse(Console.ReadLine(), out num);
                p.InStock = num;
                try
                {
                    testMain?.Product.Update(p);
                }
                catch (NotExistException str) { Console.WriteLine(str);}
  
                break;


            case "e":

                Console.WriteLine("Enter the Product ID to delete:");
                int.TryParse(Console.ReadLine(), out num);
               
                try
                {
                    testMain?.Product.Delete(num);
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
        Console.WriteLine("Please enter your choice: \n a. add Order Item. \n b. Order Item display option by ID. \n c. Product list view option. \n d. Update Order item data. \n e. Delete order item. \n f. show orderItem by Order ID");
        string? choise = Console.ReadLine();
        OrderItem item = new OrderItem();
        int num;
        switch (choise)
        {
            case "a":

                //Console.WriteLine("Please enter a Order item ID number:");
                //int.TryParse(Console.ReadLine(), out num);
                //item.ID = num;

                Console.WriteLine("Please enter the Product ID:");
                int.TryParse(Console.ReadLine(), out num);
                item.ProductID = num;

                Console.WriteLine("Please enter a Order ID:");
                int.TryParse(Console.ReadLine(), out num);
                item.OrderID = num;

                Console.WriteLine("please enter the price:");
                int.TryParse(Console.ReadLine(), out num);
                item.Price = num; 

                Console.WriteLine("Please enter the amount of order item:");
                int.TryParse(Console.ReadLine(), out num);
                item.Amount = num;

                testMain?.OrderItem.Add(item);
                 
                

                break;

            case "b":

                Console.WriteLine("Enter the Product ID:");
                int.TryParse(Console.ReadLine(), out num);
                int IDp = num;

                Console.WriteLine("Enter the Order ID:");
                int.TryParse(Console.ReadLine(), out num);

                List<OrderItem?> x = new List<OrderItem?>();
                try
                {
                    x = testMain?.OrderItem.GetAll(x => x?.OrderID == num && x?.ProductID == IDp).ToList()!;
                }
                catch (NotExistException str) { Console.WriteLine(str); }
                x.ForEach(p => Console.WriteLine(p));


                break;

            case "c":

                try
                {
                    List<OrderItem?>items = testMain?.OrderItem.GetAll().ToList() ?? throw new NullReferenceException();
                    items.ForEach(p => Console.WriteLine(p));
                }
                catch (NotExistException str) { Console.WriteLine(str); }

               
                break;

            case "d":
                int num1;
                item = new OrderItem();
                Console.WriteLine("For update please enter a Order item ID number:");
                int.TryParse(Console.ReadLine(), out num); 

                Console.WriteLine("For update please enter the Product ID:");
                int.TryParse(Console.ReadLine(), out num1);
                try
                {
                    Console.WriteLine(testMain?.OrderItem.GetByCondition(x => x?.ID == num && x?.ProductID == num1));
                }
                catch (NotExistException str) { Console.WriteLine(str);break; }   
   
                
                Console.WriteLine("Please enter the details of the Order item you want to update:");
              
                Console.WriteLine("Please enter a Order item ID number:");
                int.TryParse(Console.ReadLine(), out num);
                item.ID = num;

                Console.WriteLine("Please enter the Product ID:");
                int.TryParse(Console.ReadLine(), out num);
                item.ProductID = num;

                Console.WriteLine("Please enter a Order ID:");
                int.TryParse(Console.ReadLine(), out num);
                item.OrderID = num;

                Console.WriteLine("please enter the price:");
                int.TryParse(Console.ReadLine(), out num);
                item.Price = num;

                Console.WriteLine("Please enter the amount of order item:");
                int.TryParse(Console.ReadLine(), out num);
                item.Amount = num;
                try
                {
                    testMain?.OrderItem.Update(item);
                }
                catch (NotExistException str) { Console.WriteLine(str); }   

                break;

            case "e":

                Console.WriteLine("Enter the Order item ID to delete:");
                int.TryParse(Console.ReadLine(), out num);
                int IDd = num;
                try
                {
                    testMain?.OrderItem.Delete(IDd);
                    Console.WriteLine("succees");
                }
                catch (NotExistException str) { Console.WriteLine(str); }   
    
                break;

            case "f":
                Console.WriteLine("Enter ID of order:");
                int.TryParse(Console.ReadLine(), out num);
                List<OrderItem?> ArrOrders = testMain?.OrderItem.GetAll(x => x?.OrderID == num).ToList() ?? throw new NullReferenceException();

                try
                {
                    ArrOrders.ForEach(p => Console.WriteLine(p));
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
        string? choise = Console.ReadLine();
        Order o = new Order();
        int num2;
        switch (choise)
        {
            case "a":

                //Console.WriteLine("Please enter order ID number:");
                //int.TryParse(Console.ReadLine(), out num2);
                //o.ID = num2;

                Console.WriteLine("Please enter the CustomerName:");
                o.CustomerName = Console.ReadLine();

                Console.WriteLine("Please enter the CustomerEmail:");
                o.CustomerEmail = Console.ReadLine();

                Console.WriteLine("Please enter the CustomerAdress:");
                o.CustomerAdress = Console.ReadLine();
                o.OrderDate = DateTime.Now;//chack
            
                testMain?.Order.Add(o);
               

                break;

            case "b":

                Console.WriteLine("Enter the oredr ID:");
                int.TryParse(Console.ReadLine(), out num2);

                try
                {
                    testMain?.Order.GetByCondition(x => x?.ID == num2);
                }
                catch (NotExistException str) { Console.WriteLine(str); }   

                
                Console.WriteLine(testMain?.Order.GetByCondition(x => x?.ID == num2));

                break;

            case "c":
                try
                {
                    List<Order?> orders = testMain?.Order.GetAll().ToList() ?? throw new NullReferenceException();
                    //foreach (var order in orders)
                    //{
                    //    Console.WriteLine(testMain.Order.GetByCondition(x => x?.ID ==order?.ID));
                    //}
                    orders.ForEach(o => Console.WriteLine(o));
                }
                catch (NotExistException str) { Console.WriteLine(str); }
          
                break;
            case "d":

                o = new Order();
                Console.WriteLine("For update please enter a Order ID number:");
                int.TryParse(Console.ReadLine(), out num2);

                try
                {
                    Console.WriteLine(testMain?.Order.GetByCondition(x => x?.ID ==num2));
                }
                catch (NotExistException str) { Console.WriteLine(str); }   
  
                
                Console.WriteLine("Please enter the details of the Order  you want to update:");

                Console.WriteLine("Please enter order ID number:");
                int.TryParse(Console.ReadLine(), out num2);
                o.ID = num2;

                Console.WriteLine("Please enter the CustomerName:");
                o.CustomerName = Console.ReadLine();

                Console.WriteLine("Please enter the CustomerEmail:");
                o.CustomerEmail = Console.ReadLine();

                Console.WriteLine("Please enter the CustomerAdress:");
                o.CustomerAdress = Console.ReadLine();

                o.OrderDate = DateTime.Now;
                try
                {
                    testMain?.Order.Update(o);
                }
                catch (NotExistException str) { Console.WriteLine(str); }   
      
                break;
            case "e":

                Console.WriteLine("Enter the Order ID to delete:");
                int.TryParse(Console.ReadLine(), out num2);
                int IDd = num2;
                try
                {
                    testMain?.Order.Delete(IDd);
                }
                catch (NotExistException str) { Console.WriteLine(str); }

                break;

            default:
                break;

        }
    }
}

//------------------------------------------------->>>>> End of start with Order <<<<<--------------------------------------------------