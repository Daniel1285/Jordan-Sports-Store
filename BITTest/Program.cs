using BlApi;
using Dal;
using DalApi;
using BO;

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
            Console.WriteLine("Please enter your choice: \n a. add product to the cart. \n b. Updat the quantity of a product in the shopping cart. \n c. Confrim Order.");
            string? choise = Console.ReadLine();
            Cart c = new Cart();

            switch (choise)
            {
                case "a":

                    Console.WriteLine("Please enter a your name");
                    c.CustomerName= Console.ReadLine();
                    Console.WriteLine("Please enter tour Email.");
                    c.CustomerEmail= Console.ReadLine();    
                    Console.WriteLine("Please enter a your addres home.");
                    c.CustomerAddress= Console.ReadLine();  

                    break;

                case "b":
                    
                    int id , newAmount;
                    BO.OrderItem o = new BO.OrderItem();
                    Console.WriteLine("for wpdate please enter the following details:");
                    Console.Write("ID product: ");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        
                    }
                    catch (NotExistException str) { Console.WriteLine(str); }

                    Console.Write("Amount of product to update: ");
                    newAmount = int.Parse(Console.ReadLine());    
                    testMain.Cart.UpdateAmountOfProduct(c, id, newAmount);

                    
                    break;

                case "c":
                    Console.WriteLine();

                    break;


                default:
                    break;

            }
        }    

        //------------------------------------------------->>>>> End of start with Cart <<<<<--------------------------------------------------

        public static void choiceProduct()
        {

        }

        //------------------------------------------------->>>>> End of start with Product <<<<<--------------------------------------------------

        public static void choiceOrder()
        {

        }

        //------------------------------------------------->>>>> End of start with Order <<<<<--------------------------------------------------

    }

}

