using Infrastructure.Entities;
using Infrastructure.Exceptions;

namespace Tests.Unit
{
    public class BookInfoEntityTests
    {
        [Test]
        public void ChangeQuantity_IfQuantityWouldBeNegative_ThrowsNegativeNumberException()
        {
            var book = Book.Create(
                "title",
                "author",
                string.Empty,
                123);
            var bookInfo = BookInfo.Create(
                12.99,
                5,
                book);

            Assert.Throws<NegativeNumberException>(() =>
                bookInfo.ChangeQuantity(-10)
                ) ;
        }
    }
}
