﻿  
using DO;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Xml.Linq;
using static DO.Enums;

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

        static int MinOrder = 100000;
        static int MinOrderItem = 100000;


        private static int IdForOrder = MinOrder; // Automatic ID number for order

        private static int IdForOrderItem = MinOrderItem;   // Automatic ID number for order item

        public static int GetIdForOrder { get => IdForOrder++; } // get for Automatic ID number for order
        public static int GetIdForOrderItem { get => IdForOrderItem++; } // get for Automatic ID number for order item



    }

    private static readonly Random R = new Random(); // Random number generation

    internal static List<Product?> MyProducts = new List<Product?>();
    internal static List<Order?> MyOrder = new List<Order?>();
    internal static List<OrderItem?> MyOrderItem = new List<OrderItem?>();





    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="P"></param>
    private static void AddProduct() 
    {
        
        const int SIZE = 10;
        string[] namePprodcut = { "shoes - Air Jordan 1 Zoom CMFT", "shoes - Air Jordan 1 Element", "short - Air Jordan x Titan",
                                    "shoes - Air Jordan 1 Low SE kids", "hoodies - Jordan Essentials","hoodies - Jordan Essentials red", "socks - Nike Elite Crew",
                                         "socks - Nike Everyday Plus Cushioned", "shirt - Nike Pro Dri-FIT", "shirt - Nike Dri-FIT Legend"};
        Double[] priceOfProduct = { 550, 400, 250, 332.5, 210, 129.99, 49, 63, 122.5, 140 };
        Enums.Category[] categories = { Category.SHOES, Category.SHOES, Category.SHORTS, Category.SHOES, Category.HOODIES, Category.HOODIES, Category.SOCKS, Category.SOCKS, Category.SHIRTS, Category.SHIRTS };
        int[] AmountInSoke = { 0, 7, 25, 15, 20, 19, 10, 16, 3, 10 };
        
       
        Enumerable.Range(0, SIZE).ToList().ForEach(i =>
        {
            Product p = new Product
            {
                ID = R.Next(100000, 900000),
                Name = namePprodcut[i],
                Price = priceOfProduct[i],
                Category = categories[i],
                InStock = AmountInSoke[i],
            };
            if (CheckID(p.ID))
            {
                MyProducts.Add(p);
            }
        });



    }

    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="O_i"></param>
    private static void Add_OrderItem()
    {

        
        OrderItem NewOrderItem = new OrderItem();
        MyOrder.ToList().ForEach(order =>
        {
            Enumerable.Range(0, R.Next(1, 4)).ToList().ForEach(i =>
            {
                NewOrderItem.ID = Config.GetIdForOrderItem;
                NewOrderItem.OrderID = (int)order?.ID!;
                Product? product = MyProducts[R.Next(0, MyProducts.Count)];
                NewOrderItem.ProductID = (int)product?.ID!;
                NewOrderItem.Amount = R.Next(1, 4);
                NewOrderItem.Price = (double)product?.Price!;
                MyOrderItem.Add(NewOrderItem);
            });
        });



    }



    /// <summary>
    /// A method that adds an object to an array
    /// </summary>
    /// <param name="O"></param>
    private static void AddOrder()
    {

        const int SIZE = 20;
        string[] Name = { "Nati", "Rez", "Daniel", "Asaf", "Ron", "Dor", "eyal", "David", "Komar", "Osher", "Yotam", "Shoam", "Neomi", "Toar", "Fani", "Zion", "Avi", "Dror", "Yaakov", "Michal" };
        string[] Address = { "aaa 1", "bbb 2", "ccc 3", "ddd 4", "eee 5", "fff 6", "ggg 7", "hhh 8", "iii 9", "jjj 10", "kkk 11", "lll 12", "mmm 13", "nnn 14", "ooo 15", "ppp 16", "qqq 17", "rrr 18", "sss 19", "ttt 20" };
        int num = R.Next(1, 30);
        DateTime date = DateTime.Now;
        DateTime date2 = new DateTime(2022, 10, R.Next(1, 30));
        TimeSpan t = date - date2;

        
        Enumerable.Range(0, SIZE).ToList().ForEach(i =>
        {
            Order newOrder = new Order
            {
                ID = Config.GetIdForOrder,
                CustomerName = Name[i],
                CustomerAdress = Address[i],
                CustomerEmail = Name[i] + "@gmail.com",
                OrderDate = date - new TimeSpan(num, 0, 0, 0)
            };

            if (i < 16)
                newOrder.ShipDate = newOrder.OrderDate + new TimeSpan(R.Next(2, 4), 0, 0, 0);

            if (i < 10)
                newOrder.DeliveryrDate = newOrder.ShipDate + new TimeSpan(R.Next(1, 3), 0, 0, 0);

            MyOrder.Add(newOrder);
        });


    }

    /// <summary>
    /// constructor initialization
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
        AddProduct();
        AddOrder();
        Add_OrderItem();
    }


    /// <summary>
    /// if Id not exist return true.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool CheckID(int? id)
    {
        MyProducts.ForEach(p =>
        {
            if (id == p?.ID)
            {
                throw new AlreadyExistException("ID of product alrady exist");
            }
        });
        return true;
    }
}
