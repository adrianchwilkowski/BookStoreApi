using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Tests
{
    public class BookInfoRepositoryTests : IntegrationTestBase
    {
        [Test]
        public async Task CanGetBookInfoList()
        {
            var result = await bookInfoRepository.GetBookInfoList();

            Assert.True(result.Any());
        }

        [Test]
        public async Task CanGetBookInfoByBookId()
        {
            var books = await booksRepository.GetBooks();
            var book = books.First();

            var result = await bookInfoRepository.GetByBookId(book.Id);

            Assert.True(result.Any());
        }
        [Test]
        public async Task GetByBookId_IfBookInfoDoesNotExist_ReturnEmptyList()
        {
            Assert.ThrowsAsync<NotFoundException>( async () =>
            {
                await bookInfoRepository.GetByBookId(Guid.NewGuid());
            });
        }
        [Test]
        public async Task CanCreateBookInfo()
        {
            var books = await booksRepository.GetBooks();
            var book = books.First();

            var bookInfoList = await bookInfoRepository.GetBookInfoList();
            var count = bookInfoList.Count;

            var bookInfo = BookInfo.Create(39.99,25,book);
            await bookInfoRepository.Create(bookInfo);

            var listAfterCreate = await bookInfoRepository.GetBookInfoList();

            Assert.True(listAfterCreate.Count == count+1);
        }
    }
}
