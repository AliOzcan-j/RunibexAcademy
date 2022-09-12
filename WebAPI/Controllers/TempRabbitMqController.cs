using Core.Utilities.MessageBrokers;
using Core.Utilities.MessageBrokers.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempRabbitMqController : ControllerBase
    {
        private IMessageBroker _messageBroker;

        public TempRabbitMqController(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        [HttpGet("publish")]
        public async Task<IActionResult> Publish(string email)
        {
            _messageBroker.Publish(new UserRegisteredEvent() { UserEmail="deneme@deneme.com"});
            return Ok();
        }
    }
}
