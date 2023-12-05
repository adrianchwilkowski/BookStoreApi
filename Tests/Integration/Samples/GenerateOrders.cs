using Infrastructure.Entities;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading.Tasks;

//namespace Tests.Integration.Samples
//{
//    public class GenerateOrders
//    {
//        private List<OrderedItem> _orderedBooks;
//        private List<BookInfo> _bookInfo;
//        public GenerateOrders(List<BookInfo> bookInfo, List<OrderedItem> orderedBooks)
//        {
//            _bookInfo = bookInfo;
//            _orderedBooks = orderedBooks;
//        }
//        public List<Order> Generate()
//        {
//            List<Order> orders = new List<Order>();
//            double price = 0;
//            var userId = Guid.NewGuid();
//            foreach (var orderedBook in _orderedBooks)
//            {
//                try
//                {
//                    price = GetBookPriceByBookId(_bookInfo, orderedBook.Id);
//                    var order = orders.First(o => o.Id == orderedBook.OrderId);
//                }
//                catch (NotFoundException ex)
//                {
//                    throw ex;
//                }
//                catch (Exception)
//                {
//                    CreateOrder(orders, price, orderedBook.Quantity, 1, userId);
//                }
//            }
//            return orders;
//        }
//        private void CreateOrder(List<Order> orders, double price, int amount, int deliveryType, Guid userId)
//        {
//            var order = new Order
//            {
//                Id = Guid.NewGuid(),
//                FullCost = price * amount,
//                DeliveryType = deliveryType,
//                UserId = userId
//            };
//            orders.Add(order);
//        }
//        private void AddToOrder(Order order, double price, int amount)
//        {
//            order.FullCost += price * amount;
//        }
//        private double GetBookPriceByBookId(List<BookInfo> bookInfo, Guid bookId)
//        {
//            try
//            {
//                var result = bookInfo.First(o => o.Id == bookId).Price;
//                return result;
//            }
//            catch (Exception)
//            {
//                throw new NotFoundException("The book with given Id doesn't exist");
//            }
//        }
//        private int GetAmountOfBooksAddedToOrder(List<OrderedItem> orderedBooks, Guid orderId)
//        {
//            try
//            {
//                var result = orderedBooks.First(o => o.Id == orderId).Quantity;
//                return result;
//            }
//            catch (Exception)
//            {
//                throw new NotFoundException("The set of items with given Id doesn't exist");
//            }
//        }
//    }
//}
