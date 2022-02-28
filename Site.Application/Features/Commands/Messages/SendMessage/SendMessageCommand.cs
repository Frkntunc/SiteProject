using MediatR;
using Site.Application.Models.Message;

namespace Site.Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommand:IRequest
    {
        public SendMessageCommand(int userId,SendMessageModel sendMessageModel)
        {
            UserId = userId;
            Content = sendMessageModel.Content;
            To = sendMessageModel.To;
        }

        public int UserId { get; set; }
        public string Content { get; set; }
        public string To { get; set; }
        public string From { get; set; }
    }
}
