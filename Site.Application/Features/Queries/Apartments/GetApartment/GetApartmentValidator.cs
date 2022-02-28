using FluentValidation;

namespace Site.Application.Features.Queries.Apartments.GetApartment
{
    public class GetApartmentValidator:AbstractValidator<GetApartmentQuery>
    {
        public GetApartmentValidator()
        {
            RuleFor(a => a.ID).NotNull().GreaterThan(0);
        }
    }
}
