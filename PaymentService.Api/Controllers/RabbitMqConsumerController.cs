using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentService.Application.Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Site.Domain.Dtos;
using System.Text;

namespace PaymentService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqConsumerController : ControllerBase
    {
        private readonly IRabbitMqConsumer _rabbitMqConsumer;
        private readonly ConnectionFactory factory;
        private readonly IConnection connection;

        public RabbitMqConsumerController(IRabbitMqConsumer rabbitMqConsumer)
        {
            _rabbitMqConsumer = rabbitMqConsumer;

            factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "test",
                Password = "test"
            };
            connection = factory.CreateConnection();
        }

        [HttpGet]
        public IActionResult Consume()
        {

            using (var channel = this.connection.CreateModel())
            {
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume("payment", true, consumer);
                consumer.Received += (sender, e) =>
                {

                    var body = e.Body.ToArray();
                    var jsonString = Encoding.UTF8.GetString(body);
                    var creditCard = JsonConvert.DeserializeObject<CreditCardDto>(jsonString);

                    _rabbitMqConsumer.Handle(creditCard);
                };
            }

            return Ok();
        }
    }
}
