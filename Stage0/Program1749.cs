namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {      
            welcome1749();
            welcome9255();

            Console.ReadKey();
        }
          
        static partial void welcome9255();
        private static void welcome1749()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, wlcome to my first console application", userName);
        }
    }
}