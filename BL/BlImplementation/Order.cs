using BlApi;
using Dal;
using DalApi;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation
{
    internal class Order: BlApi.IOrder
    {
        private IDal Dal = new DalList();
        /// <summary>
        /// Brings a list of orders from the data layer and builds an order list 
        /// OrderForList on this data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> GetOrderLists()
        {
            List<DO.Order> orders = new List<DO.Order>();
            List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
            try
            {
                orders = Dal.Order.GetAll().ToList();
            }
            catch (Exception) { }
     
            foreach (var item in orders)
            {
                BO.OrderForList order = new BO.OrderForList()
                {
                    ID = item.ID,
                    CustomerName= item.CustomerName,
                    Status = getStatus(item),
                };
                List<DO.OrderItem> orderItem = Dal.OrderItem.GetOrdersItem(item.ID);
                foreach (var i in orderItem)
                {
                    order.AmountOfItems += i.Amount;
                    order.TotalPrice += i.Price * i.Amount;
                }
                ordersForList.Add(order);
            }

            return ordersForList;
        }
        /// <summary>
        /// Receives data if the data is correct we will ask do for an order 
        /// and build an order of type bo
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.Order GetOrder(int orderId)
        {
            List<DO.OrderItem> OrderItemFromDo = new List<DO.OrderItem>();
            DO.Order OrderFromDo = new DO.Order();
            
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
            try
            {
                 OrderFromDo = Dal.Order.GetByID(orderId);

            }
            catch (BO.NotExistException ex) { Console.WriteLine(ex); }
       
            OrderItemFromDo = Dal.OrderItem.GetAll().ToList();

            BO.Order o = new BO.Order
            {
                ID = OrderFromDo.ID,
                CustomerName = OrderFromDo.CustomerName,
                CustomerEmail = OrderFromDo.CustomerEmail,
                CustomerAddress = OrderFromDo.CustomerAdress,
                OrderDate = OrderFromDo.OrderDate,
                Status = getStatus(OrderFromDo),
                ShipDate = OrderFromDo.ShipDate,
                DeliveryDate = OrderFromDo.DeliveryrDate,
                Items = FromDotToBoOrderItem(OrderFromDo.ID, OrderItemFromDo).Item1,
                TotalPrice = FromDotToBoOrderItem(OrderFromDo.ID, OrderItemFromDo).Item2,

            };
            return o;
        }
        /// <summary>
        /// Receives a figure if the figure is correct updates the 
        /// shipping date of the order both bo and do
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.Order ShippingUpdate(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal.Order.GetByID(orderId);
                order1 = GetOrder(orderId);
            }
            catch (BO.NotExistException ex) { Console.WriteLine(ex); }

            if (order.ShipDate == DateTime.MinValue)
            {
                order.ShipDate = DateTime.Now; 
                order1.ShipDate = DateTime.Now;    
                
            }

            return order1;
    
         }
        /// <summary>
        /// Receives a figure if the figure is correct updates the delivery 
        /// date of the order both bo and do
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.Order SupplyUpdateOrder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal.Order.GetByID(orderId);
                order1 = GetOrder(orderId);
            }
            catch (BO.NotExistException ex) { Console.WriteLine(ex); }

            if (order.DeliveryrDate == DateTime.MinValue)
            {
                order.DeliveryrDate = DateTime.Now;
                order1.DeliveryDate = DateTime.Now;
            }

            return order1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        public BO.OrderTracking TrackingOtder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero");
            DO.Order order = new DO.Order();
            BO.OrderTracking order1 = new BO.OrderTracking();
            Tuple<DateTime, BO.Enums.OrderStatus>? p;
            try
            {
                order = Dal.Order.GetByID(orderId);
            }
            catch (BO.NotExistException ex) { Console.WriteLine(ex); }

            order1.ID= orderId; 
            order1.Status = GetOrder(orderId).Status;
            p = new Tuple<DateTime, BO.Enums.OrderStatus>(
                (DateTime)order.OrderDate,
                (BO.Enums.OrderStatus)order1.Status);
            order1.Pair.Add(p);
            return order1;  
            
            
        }
        /// <summary>
        /// Helper function for finding status
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private BO.Enums.OrderStatus getStatus(DO.Order order)
        {
            if (order.DeliveryrDate != DateTime.MinValue)
                return BO.Enums.OrderStatus.Order_Provided;
            if (order.ShipDate != DateTime.MinValue)
                return BO.Enums.OrderStatus.Order_Sent;
            return BO.Enums.OrderStatus.Order_Confirmed;
        }

        /// <summary>
        /// An auxiliary function for calculating a general amount and also converting an order by heart 
        /// so that we can insert into the order the items of its type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private (List<BO.OrderItem> , double) FromDotToBoOrderItem(int id, List<DO.OrderItem>? items)

        {
            items = (List<DO.OrderItem>?)Dal.OrderItem.GetAll();
            List<BO.OrderItem>? orderItems = new List<BO.OrderItem>();
            double sum = 0;
            foreach (var i in items)
            {
                if (id == i.OrderID)
                {
                    BO.OrderItem orderItem = new BO.OrderItem()
                    {
                        ID = i.ID,
                        Amount = i.Amount,
                        ProductID = i.ProductID,
                        Price = i.Price,
                        Name = Dal.Product.GetByID(i.ProductID).Name,
                    };
                    sum += i.Price;
                    orderItems.Add(orderItem);
                }

            }
            return (orderItems, sum);
        }

    }
}
