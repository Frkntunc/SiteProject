using FluentValidation;

namespace Site.Application.Features.Commands.Apartments.AddApartment
{
    public class AddApartmentValidator:AbstractValidator<AddApartmentCommand>
    {
        public AddApartmentValidator()
        {
            RuleFor(a => a.ApartmentNumber).NotEmpty();
            RuleFor(a => a.Blok).NotEmpty();
            RuleFor(a => a.Floor).NotEmpty();
            RuleFor(a => a.Status).NotEmpty();
            RuleFor(a => a.Owner).NotEmpty();
            RuleFor(a => a.Type).NotEmpty();
        }
    }
}
