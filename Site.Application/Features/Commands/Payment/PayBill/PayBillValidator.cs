using FluentValidation;

namespace Site.Application.Features.Commands.Payment.PayBill
{
    public class PayBillValidator : AbstractValidator<PayBillCommand>
    {
        public PayBillValidator()
        {
            RuleFor(p => p.CreditCardNumber).Length(16);
            RuleFor(p => p.Cvc).Length(3);
            RuleFor(p => p.ExpireDate).NotNull();
            RuleFor(p => p.Month).NotNull().GreaterThan(0).LessThanOrEqualTo(12);
            RuleFor(p => p.Pay).NotNull().GreaterThan(0);
        }
    }
}
