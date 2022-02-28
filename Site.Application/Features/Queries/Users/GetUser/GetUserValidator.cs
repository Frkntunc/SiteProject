using FluentValidation;

namespace Site.Application.Features.Queries.Users.GetUser
{
    public class GetUserValidator:AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(u => u.ID).NotEmpty().GreaterThan(0);
        }
    }
}
