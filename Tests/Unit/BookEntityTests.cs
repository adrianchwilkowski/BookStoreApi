using Infrastructure.Entities;

namespace Tests.Unit
{
    public class BookEntityTests
    {
        [Test]
        public void Create_BookInfoListIsEmpty()
        {
            var book = Book.Create(
                "title",
                "author",
                string.Empty,
                123);
            Assert.True(!book.BookInfoList.Any());
        }
    }
}
