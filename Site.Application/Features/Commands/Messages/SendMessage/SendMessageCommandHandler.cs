using AutoMapper;
using MediatR;
using FluentValidation;
using Site.Application.Contracts.Persistence.Repositories.Messages;
using System.Threading;
using System.Threading.Tasks;
using Site.Domain.Entities;
using Site.Domain.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Site.Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SendMessageValidator _validator;

        public SendMessageCommandHandler(IMessageRepository messageRepository, UserManager<User> userManager, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
            _mapper = mapper;
            _validator = new SendMessageValidator();
        }
        
        public async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            request.From = user.Email;

            var message = _mapper.Map<Message>(request);
            
            await _messageRepository.AddAsync(message);

            return Unit.Value;
        }
    }
}
