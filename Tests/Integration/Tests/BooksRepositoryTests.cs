using Infrastructure.Entities;
using Infrastructure.Migrations;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Tests
{
    public class BooksRepositoryTests : IntegrationTestBase
    {
        [Test]
        public void CanGetBookList()
        {
            var context = TestDbContext();
            var booksRepository = new BooksRepository(context);
            var data = JsonConvert.DeserializeObject(File.ReadAllText(samplesDir+"Books.Json"));
            var bookList = new List<Book>();
            if (data is not null && data is System.Collections.IEnumerable enumerable)
            {
                foreach (var i in enumerable)
                {
                    if (i is JObject jObject)
                    {
                        var pages = (int)jObject["pages"];
                        var title = (string)jObject["title"];
                        var author = (string)jObject["author"];
                        var description = (string)jObject["description"];

                        var book = Book.Create(
                            title,
                            author,
                            description,
                            pages
                            );
                        bookList.Add(book);
                    }
                }
            }
            context.Books.AddRange(bookList);
            context.SaveChanges();

            var result = booksRepository.GetBooks();

            Assert.True(result.Any());
        }
    }
}
