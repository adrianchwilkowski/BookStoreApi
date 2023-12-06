using Infrastructure.Entities;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Unit
{
    public class OrderedItemEntityTests
    {
        [Test]
        public void ChangeQuantity_CannotBeNegative() 
        {
            var order = Order.Create(
                3,
                Guid.NewGuid());
            var book = Book.Create(
                "title",
                "author",
                string.Empty,
                123);
            var bookInfo = BookInfo.Create(
                12.99,
                5,
                book);

            var item = OrderedItem.Create(
                bookInfo,
                order
                );

            Assert.Throws<NegativeNumberException>(() => item.ChangeQuantity(-1));
        }
    }
}
