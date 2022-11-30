using BlApi;
using Dal;
using DalApi;
using System.Linq;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        private IDal Dal = new DalList();
        public BO.Cart AddProdctToCatrt(BO.Cart cart, int id)
        {
            DO.Product product1 = new DO.Product();
            List<DO.OrderItem> orderItem1 = Dal.OrderItem.GetAll().ToList();
            try
            {
                product1 = Dal.Product.GetByID(id);
            }
            catch (BO.NotExistException ex)
            {
                Console.WriteLine(ex);
            }
            if (cart.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    if (id == item.ProductID)
                    {
                        if (product1.InStock > item.Amount)
                        {
                            item.Amount++;
                            item.Totalprice += item.Price;
                            cart.TotalPrice += item.Price;
                            return cart;
                        }

                    }
                }
            }
            if(product1.InStock > 0)
            {
                foreach (var item in orderItem1)
                {
                    if(id == item.ProductID)
                    {
                        BO.OrderItem orderItemBo = new BO.OrderItem
                        {
                            ID = item.ID,
                            Name = product1.Name,
                            ProductID = item.ProductID,
                            Price = item.Price,
                            Amount = 1,
                            Totalprice = item.Price
                        };
                        cart.Items.Add(orderItemBo);
                        Console.WriteLine("sucsess");
                        break;
                    }
                }
                     
            }
            return cart;
        }

        public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int id, int newAmount)
        {
            DO.Product product1 = new DO.Product();
            try
            {
                product1 = Dal.Product.GetByID(id);
            }
            catch (Exception)
            {

                throw;
            }
            foreach (var item in cart.Items)
            {
                if (id == item.ProductID)
                {
                    if(newAmount > item.Amount)
                    {
                        if(product1.InStock >= item.Amount + newAmount)
                        {
                            item.Amount += newAmount;
                            item.Totalprice += item.Price * newAmount;
                            cart.TotalPrice += item.Price * newAmount;
                        }
                    }
                    else if(newAmount < item.Amount && newAmount != 0)
                    {
                        item.Amount -= newAmount;
                        item.Totalprice -= item.Price * newAmount;
                        cart.TotalPrice -= item.Price * newAmount;
                    }
                    else
                    {
                        cart.TotalPrice -= item.Totalprice;
                        cart.Items.Remove(item);   

                    }
                }

            }
            return cart;
        }
        public void ConfirmOrder(BO.Cart cart)
        {
            DO.Order order1 = new DO.Order()
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryrDate =DateTime.MinValue,
            };
            int orderId = Dal.Order.Add(order1);
            foreach(var item in cart.Items)
            {
                DO.OrderItem orderItem1 = new DO.OrderItem()
                {
                    ID = item.ID,
                    ProductID = item.ProductID,
                    OrderID = orderId,
                    Price = item.Price,
                    Amount = item.Amount,
                };
                Dal.OrderItem.Add(orderItem1);
            }
            DO.Product product1 = new DO.Product();
            foreach (var item in cart.Items)
            {
                product1 = Dal.Product.GetByID(item.ProductID);
                product1.InStock -= item.Amount;
                Dal.Product.Update(product1);
            }


        }

    }
}
