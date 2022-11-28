using BlApi;
using Dal;
using DalApi;

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
                ordersForList.Add(new BO.OrderForList()
                {
                    ID = item.ID,
                    CustomerName= item.CustomerName,
                    //Status = Enums.OrderStatus.Order_Confirmed,
                    //AmountOfItems = 
                    //TotalPrice =

                });
            }

            return ordersForList;
        }

        public Order GetOrder(int orderId)
        {
            DO.OrderItem OrderItemFromDo = new DO.OrderItem();
            DO.Order OrderFromDo = new DO.Order();
            
            if (orderId < 0) throw new BO.IdSmallThanZeroException("#############");
            try
            {
                 OrderFromDo = Dal.Order.GetByID(orderId);

            }
            catch (BO.NotExistException) { Console.WriteLine("#########"); }
            try
            {
                OrderItemFromDo = Dal.OrderItem.GetByID(orderId); 
            }
            catch (BO.NotExistException) { Console.WriteLine("##########"); }
            BO.Order o = new BO.Order
            {
                ID = OrderFromDo.ID,
                CustomerName = OrderFromDo.CustomerName,
                CustomerEmail = OrderFromDo.CustomerEmail,
                CustomerAddress = OrderFromDo.CustomerAdress,
                OrderDate = OrderFromDo.OrderDate,
                //Status = BO.Enums.OrderStatus,
                //PaymentDate = NOT IN DAL,
                ShipDate = OrderFromDo.ShipDate,
                DeliveryDate = OrderFromDo.DeliveryrDate,
                //Items = (Dal.OrderItem).GetAll().ToList(),
                TotalPrice =  ,

                
            }

        }

        public BO.Order ShippingUpdate(int orderid)
        {

        }

        public BO.Order SupplyUpdateOrder(int orderid)
        {

        }

        public BO.OrderTracking TrackingOtder(int orderid)
        {

        }



    }
}
