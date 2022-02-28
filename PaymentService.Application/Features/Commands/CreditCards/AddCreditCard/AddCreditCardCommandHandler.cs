using AutoMapper;
using MediatR;
using FluentValidation;
using PaymentService.Application.Contracts.Persistence.Repositories.CreditCards;
using PaymentService.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Application.Features.Commands.CreditCards.AddCreditCard
{
    public class AddCreditCardCommandHandler : IRequestHandler<AddCreditCardCommand>
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly IMapper _mapper;
        private readonly AddCreditCardValidator _validator;

        public AddCreditCardCommandHandler(ICreditCardRepository creditCardRepository, IMapper mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _validator = new AddCreditCardValidator();
        }

        public async Task<Unit> Handle(AddCreditCardCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var creditCard = _mapper.Map<CreditCard>(request);
            _creditCardRepository.Add(creditCard);

            return Unit.Value;
        }
    }
}
