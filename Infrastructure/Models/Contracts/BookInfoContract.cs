using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Contracts
{
    public class BookInfoContract
    {
        public Guid BookId { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public static BookInfoContract FromDomainModel(BookInfo bookInfo)
        {
            var result = new BookInfoContract()
            {
                BookId = bookInfo.BookId,
                Price = bookInfo.Price,
                Quantity = bookInfo.Quantity
            };
            return result;
        }
    }
}
