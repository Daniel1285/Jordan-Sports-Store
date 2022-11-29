using BlApi;
using Dal;
using DalApi;
using System.Diagnostics.CodeAnalysis;

namespace BlImplementation
{
    internal class Order: BlApi.IOrder
    {
        private IDal Dal = new DalList();

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

        public BO.Order GetOrder(int orderId)
        {
            List<DO.OrderItem> OrderItemFromDo = new List<DO.OrderItem>();
            DO.Order OrderFromDo = new DO.Order();
            
            if (orderId < 0) throw new BO.IdSmallThanZeroException("#############");
            try
            {
                 OrderFromDo = Dal.Order.GetByID(orderId);

            }
            catch (BO.NotExistException) { Console.WriteLine("#########"); }
       
            OrderItemFromDo = Dal.OrderItem.GetAll().ToList();

            BO.Order o = new BO.Order
            {
                ID = OrderFromDo.ID,
                CustomerName = OrderFromDo.CustomerName,
                CustomerEmail = OrderFromDo.CustomerEmail,
                CustomerAddress = OrderFromDo.CustomerAdress,
                OrderDate = OrderFromDo.OrderDate,
                Status = getStatus(OrderFromDo),
                //PaymentDate = DateTime.Now,
                ShipDate = OrderFromDo.ShipDate,
                DeliveryDate = OrderFromDo.DeliveryrDate,
                Items = FromDotToBoOrderItem(OrderFromDo.ID, OrderItemFromDo).Item1,
                TotalPrice = FromDotToBoOrderItem(OrderFromDo.ID, OrderItemFromDo).Item2,

            };
            return o;
        }

        public BO.Order ShippingUpdate(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("#############");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal.Order.GetByID(orderId);
                order1 = GetOrder(orderId);
            }
            catch (BO.NotExistException) { Console.WriteLine("#########"); }

            if (order.ShipDate == DateTime.MinValue)
            {
                order.ShipDate = DateTime.Now; 
                order1.ShipDate = DateTime.Now;    
                
            }

            return order1;

    }

        public BO.Order SupplyUpdateOrder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("#############");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal.Order.GetByID(orderId);
                order1 = GetOrder(orderId);
            }
            catch (BO.NotExistException) { Console.WriteLine("#########"); }

            if (order.DeliveryrDate == DateTime.MinValue)
            {
                order.DeliveryrDate = DateTime.Now;
                order1.DeliveryDate = DateTime.Now;
            }

            return order1;
        }

        public BO.OrderTracking TrackingOtder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("#############");
            DO.Order order = new DO.Order();
            BO.OrderTracking order1 = new BO.OrderTracking();
            try
            {
                order = Dal.Order.GetByID(orderId);
            }
            catch (BO.NotExistException) { Console.WriteLine("#########"); }

            order1.ID= orderId; 
            order1.Status = GetOrder(orderId).Status;
            order1.Pair
        }

        private BO.Enums.OrderStatus getStatus(DO.Order order)
        {
            if (order.DeliveryrDate != DateTime.MinValue)
                return BO.Enums.OrderStatus.Order_Provided;
            if (order.ShipDate != DateTime.MinValue)
                return BO.Enums.OrderStatus.Order_Sent;
            return BO.Enums.OrderStatus.Order_Confirmed;
        }

        
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
