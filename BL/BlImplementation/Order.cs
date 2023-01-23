using BlApi;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class Order: BlApi.IOrder
    {
        private DalApi.IDal? Dal = DalApi.Factory.Get();

        #region Get order list
        /// <summary>
        /// Brings a list of orders from the data layer and builds an order list 
        /// OrderForList on this data
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.OrderForList?> GetOrderLists()
        {

            
            var ordersForList1 = from DOOrder in Dal?.Order.GetAll()
                                 let sumAmount = Dal?.OrderItem.GetAll(x => x?.OrderID == DOOrder?.ID).Sum(x => x?.Amount)
                                 let sumTotalPrice = Dal?.OrderItem.GetAll(x => x?.OrderID == DOOrder?.ID).Sum(x => x?.Price * x?.Amount)
                                 select new BO.OrderForList {
                                     ID = (int)DOOrder?.ID!,
                                     CustomerName = DOOrder?.CustomerName,
                                     Status = getStatus(DOOrder),
                                     AmountOfItems = (int)sumAmount,
                                     TotalPrice = (double)sumTotalPrice,
                                 };

            return ordersForList1;
        }
        #endregion

        #region Get Order
        /// <summary>
        /// Receives data if the data is correct we will ask do for an order 
        /// and build an order of type bo
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order GetOrder(int orderId)
        {
            List<DO.OrderItem?> OrderItemFromDo = new List<DO.OrderItem?>();
            DO.Order? OrderFromDo = new DO.Order();
            
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
            try
            {
                 OrderFromDo = Dal?.Order.GetByCondition(x => x?.ID == orderId);

            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("",ex); }
       
            OrderItemFromDo = Dal?.OrderItem.GetAll().ToList() ?? throw new NullReferenceException();

            BO.Order o = new BO.Order
            {
                ID = (int)OrderFromDo?.ID!,
                CustomerName = OrderFromDo?.CustomerName,
                CustomerEmail = OrderFromDo?.CustomerEmail,
                CustomerAddress = OrderFromDo?.CustomerAdress,
                OrderDate = OrderFromDo?.OrderDate,
                Status = getStatus(OrderFromDo),
                ShipDate = OrderFromDo?.ShipDate,
                DeliveryDate = OrderFromDo?.DeliveryrDate,
                Items = FromDotToBoOrderItem((int)OrderFromDo?.ID!, OrderItemFromDo).Item1,
                TotalPrice = FromDotToBoOrderItem((int)OrderFromDo?.ID!, OrderItemFromDo).Item2,

            };
            return o;
        }
        #endregion

        #region Get status
        /// <summary>
        /// Helper function for finding status
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private BO.Enums.OrderStatus getStatus(DO.Order? order)
        {
            if (order?.DeliveryrDate != null)
                return BO.Enums.OrderStatus.Order_Provided;
            if (order?.ShipDate != null)
                return BO.Enums.OrderStatus.Order_Sent;
            return BO.Enums.OrderStatus.Order_Confirmed;
        }
        #endregion

        #region Shipping update
        /// <summary>
        /// Receives a figure if the figure is correct updates the 
        /// shipping date of the order both bo and do
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order ShippingUpdate(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal?.Order.GetByCondition(x => x?.ID == orderId) ?? throw new NullReferenceException();
                order1 = GetOrder(orderId);
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("",ex);}
            
            if (order.ShipDate == null)
            {
                order.ShipDate = DateTime.Now;
                order1.ShipDate = DateTime.Now;
                order1.Status = getStatus(order);
                Dal.Order.Update(order);
            }

            return order1;

        }
        #endregion

        #region Supply update order
        /// <summary>
        /// Receives a figure if the figure is correct updates the delivery 
        /// date of the order both bo and do
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Order SupplyUpdateOrder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero");
            DO.Order order = new DO.Order();
            BO.Order order1 = new BO.Order();
            try
            {
                order = Dal?.Order.GetByCondition(x => x?.ID == orderId) ??throw new NullReferenceException();
                order1 = GetOrder(orderId);
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }

            if (order.DeliveryrDate == null && order.ShipDate != null)
            {
                order.DeliveryrDate = DateTime.Now;
                order1.DeliveryDate = DateTime.Now;
                Dal.Order.Update(order);
                order1.Status = getStatus(order);
            }

            return order1;
        }
        #endregion

        #region Tracking otder
        /// <summary>
        /// Return tracking order. 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="BO.IdSmallThanZeroException"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.OrderTracking TrackingOtder(int orderId)
        {
            if (orderId < 0) throw new BO.IdSmallThanZeroException("ID small than zero");
            DO.Order? order = new DO.Order();
            BO.OrderTracking order1 = new BO.OrderTracking();
            order1.Pair = new List<Tuple<DateTime, BO.Enums.OrderStatus>?>();
            Tuple<DateTime, BO.Enums.OrderStatus>? p;
            try
            {
                order = Dal?.Order.GetByCondition(x => x?.ID == orderId) ?? throw new NullReferenceException();
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }

            order1.ID= orderId; 
            order1.Status = GetOrder(orderId).Status;
            p = new Tuple<DateTime, BO.Enums.OrderStatus>(
                 (DateTime)order?.OrderDate!,order1.Status);
            order1.Pair.Add(p);
            return order1;
        }
        #endregion

        #region From Do To Bo OrderItem
        /// <summary>
        /// An auxiliary function for calculating a general amount and also converting an order by heart 
        /// so that we can insert into the order the items of its type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private (List<BO.OrderItem?> , double) FromDotToBoOrderItem(int id, List<DO.OrderItem?> items)

        {
            items = Dal?.OrderItem.GetAll().ToList() ?? throw new NullReferenceException(); ;
           
            var orderItems = from i in items
                             where id == i?.OrderID
                             select new BO.OrderItem()
                             {
                                 ID = (int)i?.ID!,
                                 Amount = (int)i?.Amount!,
                                 ProductID = (int)i?.ProductID!,
                                 Price = (double)i?.Price!,
                                 Name = Dal.Product.GetByCondition(x => x?.ID == i?.ProductID).Name,
                                 Totalprice = (double)(i?.Amount * i?.Price)!,
                             };
            double sum = orderItems.Sum(x => x.Totalprice);
                             
            return (orderItems.ToList(), sum);
        }
        #endregion

        #region Get list of orders by condition
        /// <summary>
        /// Return List of orders By Condition.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.OrderForList?> GetListByCondition(Func<BO.OrderForList?, bool>? filter)
        {
            var p = from item in GetOrderLists()
                    where (filter!(item))
                    select item;
            return p!;
        }
        #endregion

        #region Oldest order
        /// <summary>
        /// Return oldest order Id 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int? OldestOrder()
        {
            var orders = Dal!.Order.GetAll(x => getStatus(x) != BO.Enums.OrderStatus.Order_Provided);
            if (orders == null) return null;
            orders!.OrderByDescending(x => x?.ShipDate ?? x?.OrderDate);
            try
            {
                return orders!.First()?.ID!;

            }catch (InvalidOperationException) { }
            return 0;
        }
        #endregion

        #region Delete order
        /// <summary>
        /// Deleteing a Order from the array "MyOrder".
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteOrder(int OrderId)
        {
            if (OrderId < 0) throw new BO.NotExistException("ID small than zero!");
            
            try
            {
                Dal?.Order.GetByCondition(x => x?.ID == OrderId);
            }
            catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
            
            Dal?.Order.Delete(OrderId);
        }
        #endregion
    }
}
