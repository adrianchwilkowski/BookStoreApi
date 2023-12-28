using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Repositories;

namespace BookStore.Services
{
    public interface IBookSearchService
    {
        public Task<List<Book>> GetBooks();
        public Task AddBook(Book book);
    }
    public class BookSearchService : IBookSearchService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IBookInfoRepository _bookInfoRepository;
        public BookSearchService(IBooksRepository booksRepository, IBookInfoRepository bookInfoRepository) {
            _bookInfoRepository = bookInfoRepository;
            _booksRepository = booksRepository;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _booksRepository.GetBooks();
        }
        public async Task AddBook(Book book)
        {
            try
            {
                await _booksRepository.Create(book);
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
        }
    }
}
