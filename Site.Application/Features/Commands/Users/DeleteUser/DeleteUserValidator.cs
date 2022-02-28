using FluentValidation;

namespace Site.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserValidator:AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(u => u.ID).NotEmpty().GreaterThan(0);
        }
    }
}
