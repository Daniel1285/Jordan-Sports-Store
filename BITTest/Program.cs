using BlApi;
using Dal;
using DalApi;
using BO;
using System.Threading.Channels;

namespace BITTest
{
    internal class Program
    {
        private static IBl testMain = new BlImplementation.Bl();
        static void Main(string[] args)
        {
            int? choice;
            do
            {
                Console.WriteLine("\nEnter your choice:\n 0. Exit. \n 1. Cart. \n 2. Product. \n 3. Order");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case (int)Enums.StartChoose.EXIT:
                        Console.WriteLine("Have a good day");
                        break;

                    case (int)Enums.StartChoose.CART:
                        choiceCart();
                        break;

                    case (int)Enums.StartChoose.PRODUCT:
                        choiceProduct();
                        break;
                    case (int)Enums.StartChoose.ORDER:
                        choiceOrder();
                        break;

                    default:
                        Console.WriteLine("Error Tayping");
                        break;
                }
            } while (choice != 0);
            Console.WriteLine();
        }
        //----------------------------------------------------------->>> MAIN'S FUNCTIONS <<<------------------------------------------------------------

        public static void choiceCart()
        {
            Console.WriteLine("Please enter your choice:\n" +
                              " a. add product to the cart.\n" +
                              " b. Updat the quantity of a product in the shopping cart.\n" +
                              " c. Confrim Order.");

            string? choise = Console.ReadLine();
            Cart c = new Cart();
            Console.Write("Please enter a your name: ");
            c.CustomerName = Console.ReadLine();
            Console.Write("Please enter tour Email: ");
            c.CustomerEmail = Console.ReadLine();
            Console.Write("Please enter a your addres home: ");
            c.CustomerAddress = Console.ReadLine();

            switch (choise)
            {
                case "a":

                    Console.Write("Enter the id of product you want to add to the cart: ");
                    int idProduct = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Cart.AddProdctToCatrt(c, idProduct); // ######################## ERROR ###############################

                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }

                    break;

                case "b":
                    
                    int idProduct1 , newAmount;
                    BO.OrderItem o = new BO.OrderItem();
                   // o = c.Items.Find(x => BO.OrderItem.ProductID == idProduct);//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< ----------------------------------------------
                    Console.WriteLine("For update please enter the following details:");
                    Console.Write("ID product: ");
                    idProduct1 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(o); // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<----------------------------------------------
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }

                    Console.Write("Amount of product to update: ");
                    newAmount = int.Parse(Console.ReadLine());    
                    testMain.Cart.UpdateAmountOfProduct(c, idProduct1, newAmount);

                    
                    break;

                case "c":
         
                    testMain.Cart.ConfirmOrder(c);

                    break;


                default:
                    Console.WriteLine("Error Tayping");
                    break;

            }
        }    

        //------------------------------------------------->>>>> End of start with Cart <<<<<--------------------------------------------------

        public static void choiceProduct()
        {
            Console.WriteLine("Please enter your choice:\n" +
                              " a. Get product list.\n" +
                              " b. Get product for the admin.\n" +
                              " c. Get product for the claint.\n" +
                              " d. Add product (for the admin).\n" +
                              " e. Delete product.\n" +
                              " f. Update product.");

            string? choise = Console.ReadLine();
            Cart c = new Cart();

            switch (choise)
            {
                case "a":
                    List<BO.ProductForList> products = new List<BO.ProductForList>();
                    products = testMain.Product.GetProductList().ToList();
                    products.ForEach(product => Console.WriteLine(product)); 

                    break;

                case "b":
                    Console.Write("Please enter id :");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testMain.Product.GetProduct(id));

                    }
                    catch (NotExistException ex) { Console.WriteLine(ex.Message); } 

                    break;

                case "c":
                    Console.Write("Please enter ID :");
                    int idc = int.Parse(Console.ReadLine());
                    Console.WriteLine(testMain.Product.GetProduct(c,idc)); // ######################## ERROR ###############################
                    break;

                case "d":
                    Product p = new Product();

                    Console.Write("Please enter a product ID number: ");
                    p.ID = int.Parse(Console.ReadLine());

                    Console.Write("Please enter Product Name:");
                    p.Name = Console.ReadLine();

                    Console.Write("Please enter a product price: ");
                    p.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise2 = int.Parse(Console.ReadLine());
                    p.Category = (Enums.Category)choise2;

                    Console.Write("Please enter the quantity of the product in stock: ");
                    p.InStock = int.Parse(Console.ReadLine());

                    try
                    {
                        testMain.Product.AddProduct(p); 
                    }
                    catch (AlreadyExistException str) { Console.WriteLine(str); }
                    break;

                case "e":
                    Console.Write("Enter Prodcut ID to delete: ");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Product.DeleteProduct(IdToDelete);
                        Console.WriteLine("sucsses");
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }
                    break;

                case "f":
                    p = new Product();
                    Console.Write("Enter the ID number of the product you want to update: ");
                    int ID2 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(testMain.Product.GetProduct(ID2));
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }


                    Console.WriteLine("Please enter the detials product to update:");

                    Console.Write("Please enter a product ID number: ");
                    p.ID = int.Parse(Console.ReadLine());

                    Console.Write("Please enter Product Name Product ID: ");
                    p.Name = Console.ReadLine();

                    Console.Write("Please enter a product price: ");
                    p.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise3 = int.Parse(Console.ReadLine());
                    p.Category = (Enums.Category)choise3;

                    Console.Write("Please enter the quantity of the product in stock: ");
                    p.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        testMain.Product.UpdateProduct(p);
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }

                    break;

                default:
                    Console.WriteLine("Error Tayping");
                    break;

            }
        }

        //------------------------------------------------->>>>> End of start with Product <<<<<--------------------------------------------------

        public static void choiceOrder()
        {

        }

        //------------------------------------------------->>>>> End of start with Order <<<<<--------------------------------------------------

    }

}

