using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Contracts
{
    public class BookContract
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string Author { get; private set; } = null!;
        public string? Description { get; private set; }
        public int Pages { get; private set; }
        public static BookContract FromDomainModel(Book book)
        {
            var result = new BookContract()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Pages = book.Pages
            };
            return result;
        }
    }
}
