using BookStore.Services;
using Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpPut("Create")]
        public async Task<IActionResult> Create([FromBody] int deliveryType)
        {
            try
            {
                await _ordersService.CreateOrder(deliveryType);
                return Ok();
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
