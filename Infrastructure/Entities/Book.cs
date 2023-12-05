using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string Author { get; private set; } = null!;
        public string? Description { get; private set; }
        public int Pages { get; private set; }
        public IEnumerable<BookInfo> BookInfoList { get; private set; } = null!;
        public Book Create(string title, string author, string? description, int pages)
        {
            return new Book()
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                Description = description,
                Pages = pages,
                BookInfoList = new List<BookInfo>()
            };
        }
        public void AddBookInfo(double price, int quantity)
        {
            var bookInfo = BookInfo.Create(Id, price, quantity, this);
            BookInfoList.Append(bookInfo);
        }
    }
}
