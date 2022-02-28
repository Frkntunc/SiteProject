using FluentValidation;

namespace Site.Application.Features.Queries.Bills.GetBill
{
    public class GetBillValidator:AbstractValidator<GetBillQuery>
    {
        public GetBillValidator()
        {
            RuleFor(b => b.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
