using Site.Application.Contracts.Persistence.Repositories.Commons;
using Site.Domain.Dtos;
using Site.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Site.Application.Contracts.Persistence.Repositories.Messages
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        Task<List<MessageDto>> GetMessages(int userId);
    }
}
