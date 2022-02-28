using Site.Domain.Dtos;

namespace PaymentService.Application.Consumer
{
    public interface IRabbitMqConsumer
    {
        public void Handle(CreditCardDto request);
    }
}
