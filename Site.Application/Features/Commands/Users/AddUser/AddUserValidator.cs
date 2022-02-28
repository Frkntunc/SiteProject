using FluentValidation;

namespace Site.Application.Features.Commands.Users.AddUser
{
    public class AddUserValidator:AbstractValidator<AddUserCommand>
    {
        public AddUserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.PhoneNumber).NotEmpty().Length(11);
            RuleFor(u => u.TcNo).NotEmpty().Length(11);
        }
    }
}
