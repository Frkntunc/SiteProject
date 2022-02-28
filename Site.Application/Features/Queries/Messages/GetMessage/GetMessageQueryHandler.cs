using AutoMapper;
using MediatR;
using Site.Application.Contracts.Persistence.Repositories.Messages;
using Site.Application.Models.Message;
using Site.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Site.Application.Features.Queries.Messages.GetMessage
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, List<MessageModel>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetMessageQueryHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageModel>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetMessages(request.ID);

            //Çekilen mesajlar okundu olarak işaretlendi
            Message newMessages;
            for (int i = 0; i < messages.Count; i++)
            {
                newMessages = await _messageRepository.GetByIdAsync(messages[i].Id);
                newMessages.Read = true;
                await _messageRepository.UpdateAsync(newMessages);
            }

            return _mapper.Map<List<MessageModel>>(messages);
        }
    }
}
