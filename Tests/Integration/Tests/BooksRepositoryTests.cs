using Infrastructure.Entities;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Tests.Integration.Samples;

namespace Tests.Integration.Tests
{
    public class BooksRepositoryTests : IntegrationTestBase
    {
        [Test]
        public void CanGetBookList()
        {
            var context = TestDbContext();
            Seeder.Seed(context);
            var booksRepository = new BooksRepository(context);

            var result = booksRepository.GetBooks();

            Assert.True(result.Any());
        }
    }
}
