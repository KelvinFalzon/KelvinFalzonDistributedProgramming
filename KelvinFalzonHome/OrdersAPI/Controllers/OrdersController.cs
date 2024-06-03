using OrdersAPI.Models;
using OrdersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Middleware;

namespace OrdersAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly OrdersService _ordersService;
		private readonly IJwtBuilder _jwtBuilder;
		private readonly IEncryptor _encryptor;

		public OrdersController(OrdersService ordersService, IJwtBuilder jwtBuilder, IEncryptor encryptor)
		{
            _ordersService = ordersService;
			_jwtBuilder = jwtBuilder;
			_encryptor = encryptor;
		}

        [HttpPost("newOrder")]
        public async Task<ActionResult> NewOrder([FromBody] Order orders)
        {

            var order = new Order
            {
                UserId = orders.UserId,
                MovieId = orders.MovieId,
                Genre = orders.Genre

            };

            await _ordersService.CreateAsync(order);

            return Ok();
        }

        [HttpGet("getOrder")]
        public async Task<ActionResult> GetOrderAsync([FromQuery] string userId)
        {
            var orders = await _ordersService.GetList(userId);

            if (orders == null || !orders.Any())
            {
                return NotFound("Orders not found");
            }

            return Ok(orders);
        }

    }
}
