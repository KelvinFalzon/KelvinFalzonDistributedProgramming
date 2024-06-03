using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Middleware;
namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationsService _notificationsService;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IEncryptor _encryptor;

        public NotificationsController(NotificationsService notificationsService, IJwtBuilder jwtBuilder, IEncryptor encryptor)
        {
            _notificationsService = notificationsService;
            _jwtBuilder = jwtBuilder;
            _encryptor = encryptor;
        }

        [HttpPost]
        public async Task<ActionResult> NewNotification(Notifications notifications)
        {
  
            var notification = new Notifications
            {
                UserId = notifications.UserId,
                description = notifications.description           
            };

            await _notificationsService.CreateAsync(notification);

            return Ok();
        }

        [HttpGet("getnotifications")]
        public async Task<ActionResult> GetNotificationAsync([FromQuery(Name = "userId")] string id)
        {
            var notification = await _notificationsService.GetAsync(id);

            if (notification == null)
            {
                return BadRequest("Notification not found");
            }

            return Ok(notification);
        }
    }
}
