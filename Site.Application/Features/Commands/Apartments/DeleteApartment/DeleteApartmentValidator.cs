using FluentValidation;

namespace Site.Application.Features.Commands.Apartments.DeleteApartment
{
    public class DeleteApartmentValidator:AbstractValidator<DeleteApartmentCommand>
    {
        public DeleteApartmentValidator()
        {
            RuleFor(a => a.ID).NotEmpty();
        }
    }
}
