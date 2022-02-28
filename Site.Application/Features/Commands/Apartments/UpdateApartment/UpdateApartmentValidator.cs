using FluentValidation;

namespace Site.Application.Features.Commands.Apartments.UpdateApartment
{
    public class UpdateApartmentValidator:AbstractValidator<UpdateApartmentCommand>
    {
        public UpdateApartmentValidator()
        {
            RuleFor(a => a.Id).NotEmpty().GreaterThan(0);
        }
    }
}
