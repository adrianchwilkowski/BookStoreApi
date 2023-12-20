using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Tests.Integration.Samples;

namespace Tests.Integration.Tests
{
    public class BooksRepositoryTests : IntegrationTestBase
    {
        [Test]
        public async Task CanGetBookList()
        {
            var result = await booksRepository.GetBooks();

            Assert.True(result.Any());
        }
        [Test]
        public async Task CanCreateBook()
        {
            var booksRepository = new BooksRepository(context);
            var book = Book.Create("title", "author", "", 128);

            var books = await booksRepository.GetBooks();
            var booksCount = books.Count;
            await booksRepository.Create(book);

            var booksAfterCreate = await booksRepository.GetBooks();

            Assert.True(booksAfterCreate.Count == booksCount+1);
        }
        [Test]
        public async Task CreateBook_WhileTryingAddBookWithSameId_ThrowsAlreadyExistsException()
        {
            var booksRepository = new BooksRepository(context);
            var books = await booksRepository.GetBooks();

            var book = Book.Create("title", "author", "", 128);
            await booksRepository.Create(book);

            Assert.ThrowsAsync<AlreadyExistsException>(async () => {
                await booksRepository.Create(book);
            });
        }
    }
}
