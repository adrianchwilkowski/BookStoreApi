using BookStore.Services;
using Infrastructure.Entities.Identity;
using Infrastructure.Models.Commands;
using Microsoft.AspNetCore.Authorization;
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
        

        [HttpPost("AddToOrder")]
        [Authorize]
        public async Task<ActionResult<Guid>> AddToOrder([FromBody] AddOrderItemCommand command)
        {
            try
            {
                var result = await _ordersService.AddOrderItem(command);
                return Ok(result);
            }
            catch (AccessViolationException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Create([FromQuery] int deliveryType)
        {
            try
            {
                var result = await _ordersService.CreateOrder(deliveryType);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
