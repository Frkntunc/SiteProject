using MediatR;
using Site.Application.Models.Message;
using System.Collections.Generic;

namespace Site.Application.Features.Queries.Messages.GetMessage
{
    public class GetMessageQuery:IRequest<List<MessageModel>>
    {
        public GetMessageQuery(int Id)
        {
            ID = Id;
        }

        public int ID { get; set; }
    }
}
