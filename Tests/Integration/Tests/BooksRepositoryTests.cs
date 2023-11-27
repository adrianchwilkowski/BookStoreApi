using Infrastructure.Entities;
using Infrastructure.Migrations;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            var data = JsonConvert.DeserializeObject<IEnumerable<Book>>(File.ReadAllText(samplesDir+"Books.Json"));
            context.Books.AddRange(data);
            context.SaveChanges();

            var result = booksRepository.GetBooks();

            Assert.True(result.Any());
        }
    }
}
