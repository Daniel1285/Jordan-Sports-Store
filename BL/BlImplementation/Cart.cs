using BlApi;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        private DalApi.IDal? Dal = DalApi.Factory.Get();
        
        public BO.Cart AddProdctToCatrt(BO.Cart cart, int id)
        {
            if (id < 0) throw new BO.IdSmallThanZeroException("ID small zero!");
            DO.Product? product1 = new DO.Product();
            List<DO.OrderItem?> orderItem1 = Dal?.OrderItem.GetAll().ToList()?? throw new NullReferenceException();
            try
            {
                product1 =  Dal?.Product.GetByCondition(x => x?.ID == id);
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("", ex);
            }
            if (cart.Items != null)
            {
                //foreach (var item in cart.Items)
                //{
                //    if (id == item?.ProductID)
                //    {
                //        if (product1?.InStock > item.Amount)
                //        {
                //            item.Amount++;
                //            item.Totalprice += item.Price;
                //            cart.TotalPrice += item.Price;
                //            return cart;
                //        }

                //    }
                //}
                if (cart.Items.Any(i => i?.ProductID == id))
                {
                    var item = cart.Items.FirstOrDefault(i => i?.ProductID == id);
                    if (product1?.InStock > item?.Amount)
                    {
                        item.Amount++;
                        item.Totalprice += item.Price;
                        cart.TotalPrice += item.Price;

                        cart.Items = cart.Items.Select(i => i?.ProductID == id ? item : i).ToList();
                        return cart;
                    }
                }
            }
            if(product1?.InStock > 0)
            {
                //foreach (DO.OrderItem? item in orderItem1)
                //{
                //    if(id == item?.ProductID)
                //    {
                //        BO.OrderItem orderItemBo = new BO.OrderItem
                //        {
                //            ID = (int)item?.ID!,
                //            Name = product1?.Name,
                //            ProductID = (int)item?.ProductID!,
                //            Price = (double)item?.Price!,
                //            Amount = 1,
                //            Totalprice = (double)item?.Price!
                //        };

                //        cart.Items?.Add(orderItemBo);
                //        break;
                //    }
                //}
                var item = orderItem1.FirstOrDefault(i => i?.ProductID == id);
                if (item != null)
                {
                    BO.OrderItem orderItemBo = new BO.OrderItem
                    {
                        ID = (int)item?.ID!,
                        Name = product1?.Name,
                        ProductID = (int)item?.ProductID!,
                        Price = (double)item?.Price!,
                        Amount = 1,
                        Totalprice = (double)item?.Price!
                    };

                    cart.Items?.Add(orderItemBo);
                }

            }
            return cart;
        }

        public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int id, int newAmount)
        {
            if(id < 0) throw new BO.IdSmallThanZeroException("ID small than zero!");
            if (newAmount < 0) throw new BO.AmountLessThenZero("Amount small than zero!");
            DO.Product? product1 = new DO.Product();
            try
            {
                product1 = Dal?.Product.GetByCondition(x => x?.ID == id);
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistException("", ex);
            }
            //foreach (var item in cart.Items!)
            //{
            //    if (id == item?.ProductID)
            //    {
            //        if(newAmount > item.Amount)
            //        {
            //            if (product1?.InStock >= item.Amount + newAmount)
            //            {
            //                item.Amount += newAmount;
            //                item.Totalprice += item.Price * newAmount;
            //                cart.TotalPrice += item.Price * newAmount;
            //                break;
            //            }
            //            else
            //                throw new BO.NotEnougeInStock(" Not enouge in stock!");
            //        }
            //        else if(newAmount < item.Amount && newAmount != 0)
            //        {
            //            item.Amount -= newAmount;
            //            item.Totalprice -= item.Price * newAmount;
            //            cart.TotalPrice -= item.Price * newAmount;
            //            break;
            //        }
            //        else
            //        {
            //            cart.TotalPrice -= item.Totalprice;
            //            cart.Items.Remove(item);
            //            return cart;
            //        }
            //    }

            //}
            var item = cart.Items?.FirstOrDefault(i => i?.ProductID == id);
            if (item != null)
            {
                if (newAmount > item.Amount)
                {
                    if (product1?.InStock >= item.Amount + newAmount)
                    {
                        item.Amount += newAmount;
                        item.Totalprice += item.Price * newAmount;
                        cart.TotalPrice += item.Price * newAmount;
                    }
                    else
                    {
                        throw new BO.NotEnougeInStock("Not enough in stock!");
                    }
                }
                else if (newAmount < item.Amount && newAmount != 0)
                {
                    item.Amount -= newAmount;
                    item.Totalprice -= item.Price * newAmount;
                    cart.TotalPrice -= item.Price * newAmount;
                }
                else
                {
                    cart.TotalPrice -= item.Totalprice;
                    cart.Items?.Remove(item);
                    return cart;
                }
            }
        
            return cart;
        }

        public void ConfirmOrder(BO.Cart cart,string? name, string? email, string? addres)
        {
            if (name == null) throw new BO.NameIsEmptyException("Name is empty!");
            if (addres == null) throw new BO.AddresIsempty(" Addres is empty!");
            if (!new EmailAddressAttribute().IsValid(email)) throw new BO.EmailInValidException("Email invalid worng");
            DO.Order order1 = new DO.Order()
            {
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                CustomerAdress = cart.CustomerAddress,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryrDate = null,
            };

            int orderId = Dal?.Order.Add(order1)?? throw new NullReferenceException();
            if (cart.Items != null)
            {
                //foreach (var item in cart.Items)
                //{
                //    DO.OrderItem orderItem1 = new DO.OrderItem()
                //    {
                //        ID = item!.ID,
                //        ProductID = item.ProductID,
                //        OrderID = orderId,
                //        Price = item.Price,
                //        Amount = item.Amount,
                //    };
                //    Dal.OrderItem.Add(orderItem1);
                //}
                var orderItemList = (from item in cart.Items
                                     select new DO.OrderItem()
                                     {
                                         ID = item!.ID,
                                         ProductID = item.ProductID,
                                         OrderID = orderId,
                                         Price = item.Price,
                                         Amount = item.Amount,
                                     }).ToList();
               
                orderItemList.ForEach(item => Dal.OrderItem.Add(item));
               
            }
            else
                throw new BO.NotExistException("not exists!");

            DO.Product product1 = new DO.Product();
            //foreach (BO.OrderItem? item in cart.Items)
            //{

            //    try
            //    {
            //        product1 = Dal.Product.GetByCondition(x => x?.ID == item!.ProductID);
            //        product1.InStock -= item!.Amount;
            //        Dal.Product.Update(product1);
            //    }
            //    catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
            //}
            cart.Items.ToList().ForEach(item =>
            {
                try
                {
                    product1 = Dal.Product.GetByCondition(x => x?.ID == item!.ProductID);
                    product1.InStock -= item!.Amount;
                    Dal.Product.Update(product1);
                }
                catch (DO.NotExistException ex) { throw new BO.NotExistException("", ex); }
            });
        }
    }
}
