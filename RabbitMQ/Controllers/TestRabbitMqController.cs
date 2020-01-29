using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Support;

namespace RabbitMQ.Controllers
{
    [Route("api/rabbitmqtest")]
    public class TestRabbitMqController : Controller
    {
        private readonly AmqpService amqpService;

        public TestRabbitMqController(AmqpService amqpService)
        {
            this.amqpService = amqpService ?? throw new ArgumentNullException(nameof(amqpService));
        }

        [HttpPost("")]
        public IActionResult PublishMessage([FromBody] object message)
        {
            amqpService.PublishMessage(message);
            return Ok();
        }
    }
}