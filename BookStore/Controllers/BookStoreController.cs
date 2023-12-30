using BookStore.Services;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Models.Commands;
using Infrastructure.Models.Contracts;
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
        [Authorize]
        public async Task<ActionResult<List<BookContract>>> GetBooks()
        {
            var result = await _searchService.GetBooks();
            return result;
        }

        [HttpPost("AddBook")]
        [Authorize(Policy = "ManagerPolicy")]
        public async Task<IActionResult> AddBook([FromQuery] AddBookCommand book)
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
        [HttpGet("GetBookById")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<BookContract>> GetBookById(Guid id)
        {
            try
            {
                var result = await _searchService.GetById(id);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetBookByTitle")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<BookContract>> GetBookByTitle(string title)
        {
            try
            {
                var result = await _searchService.GetByTitle(title);
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBookInfo")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<ActionResult<BookInfoContract>> GetBookInfo(Guid bookId)
        {
            try
            {
                var result = await _searchService.GetInfoByBookId(bookId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
