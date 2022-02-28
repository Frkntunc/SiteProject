using FluentValidation;

namespace Site.Application.Features.Commands.Users.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Id).NotEmpty().GreaterThan(0);
        }
    }
}
