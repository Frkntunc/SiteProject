using FluentValidation;

namespace Site.Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageValidator()
        {
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.To).EmailAddress();
        }
    }
}
