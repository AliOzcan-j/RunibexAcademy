using Core.Utilities.MessageBrokers;
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
        public IActionResult Publish()
        {
            _messageBroker.Publish("Deneme");
            return Ok();
        }
    }
}
