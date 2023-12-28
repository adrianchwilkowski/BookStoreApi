using BookStore.Services;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookStoreController : Controller
    {
        private readonly IBookSearchService _searchService;
        public BookStoreController(IBookSearchService searchService) { 
            _searchService = searchService; 
        }
        [HttpGet("GetBooks")]
        [Authorize(Policy = "ManagerPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Book>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var result = await _searchService.GetBooks();
            return result;
        }

        [HttpPost("AddBook")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> AddBook([FromQuery] Book book)
        {
            try { 
                await _searchService.AddBook(book);
                return Ok();
            }
            catch (AlreadyExistsException ex)
            {
                throw ex;
            }
        }
    }
}
