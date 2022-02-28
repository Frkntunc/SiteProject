using FluentValidation;

namespace PaymentService.Application.Features.Commands.CreditCards.AddCreditCard
{
    public class AddCreditCardValidator:AbstractValidator<AddCreditCardCommand>
    {
        public AddCreditCardValidator()
        {
            RuleFor(c => c.CreditCardNumber).Length(16);
            RuleFor(c => c.Cvc).Length(3);
            RuleFor(c => c.ExpireDate).NotEmpty();
            RuleFor(c => c.Balance).NotEmpty().GreaterThan(0);
            RuleFor(c => c.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
