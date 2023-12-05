using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderedItem
    {
        public Guid Id { get; private set; }
        public Guid BookInfoId { get; private set; }
        public Guid OrderId { get; private set; }
        public int Quantity { get; private set; }
        public BookInfo BookInfo { get; private set; } = null!;
        public Order Order{ get; private set; } = null!;
        public static OrderedItem Create(Guid bookInfoId, Guid orderId, BookInfo book, Order order)
        {
            return new OrderedItem()
            {
                Id = Guid.NewGuid(),
                BookInfoId = bookInfoId,
                OrderId = orderId,
                Quantity = 0,
                BookInfo = book,
                Order = order
            };
        }
        public void ChangeQuantity(int quantity)
        {
            if (Quantity + quantity >= 0)
            {
                Quantity += quantity;
            }
            else
            {
                throw new NegativeNumberException("Quantity cannot be negative");
            }
        }
    }
}