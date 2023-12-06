using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class BookInfo
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }

        public Book Book { get; private set; } = null!;
        public IEnumerable<OrderedItem> Items { get; private set; } = null!;

        public static BookInfo Create(double price, int quantity, Book book)
        {
            return new BookInfo
            {
                Id = Guid.NewGuid(),
                Price = price,
                Quantity = quantity,
                BookId = book.Id,
                Book = book
            };
        }
        public void ChangeQuantity(int quantity)
        {
            if(Quantity + quantity >= 0)
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
