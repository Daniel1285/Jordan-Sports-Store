using BO;

namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// Order list request (admin screen).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList?> GetOrderLists();


        /// <summary>
        /// Order details request (for manager screen and buyer screen).
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrder( int orderId);

        /// <summary>
        /// Order Shipping Update (Manager Order Management Screen).
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order ShippingUpdate(int orderid);

        /// <summary>
        /// Order Delivery Update (Admin Order Management Screen).
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public Order SupplyUpdateOrder(int orderid);

        /// <summary>
        /// Order Tracking (Manager Order Management Screen).
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public OrderTracking TrackingOtder(int orderid);


        /*  ---------------->   BONUS <--------------
        /// <summary>
        /// Order update (for admin screen),
        /// </summary>
        /// <param name="o"></param>
        public void UpdateOrder(Order o);
        */

        /// <summary>
        /// Get list of Orders by condition check. 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<OrderForList?> GetListByCondition(Func<OrderForList?, bool>? filter);

        /// <summary>
        /// Return the order with oldest time.
        /// </summary>
        /// <returns></returns>
        public int? OldestOrder();


        /// <summary>
        /// Delet order
        /// </summary>
        /// <param name="OrderId"></param>
        public void DeleteOrder(int OrderId);

    }   

}
