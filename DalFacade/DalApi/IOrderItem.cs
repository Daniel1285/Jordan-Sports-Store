using DO;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        /// <summary>
        /// Returns a Order Item by ID number of product and order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public OrderItem GetByTwoID(int orderId, int productID);

        /// <summary>
        /// Method of reading a list/array of order details according to the ID number of the order.
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public List<OrderItem?> GetOrdersItem(int orderID);

    }
}
