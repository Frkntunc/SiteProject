using PaymentService.Application.Contracts.Persistence.Repositories.CreditCards;
using Site.Domain.Dtos;

namespace PaymentService.Application.Consumer
{
    public class RabbitMqConsumerHandler:IRabbitMqConsumer
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public RabbitMqConsumerHandler(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public void Handle(CreditCardDto request)
        {
            var creditCard = _creditCardRepository.GetFilterBy(c => c.UserId == request.UserId);

            if (string.Equals(creditCard.CreditCardNumber, request.CreditCardNumber) && string.Equals(creditCard.Cvc, request.Cvc))
                creditCard.Balance = creditCard.Balance - request.Pay;

            _creditCardRepository.Update(creditCard, creditCard.ID.ToString());

        }
    }
}
