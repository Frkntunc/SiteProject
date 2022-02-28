using Site.Application.Contracts.Persistence.Repositories.Messages;
using Site.Domain.Dtos;
using Site.Domain.Entities;
using Site.Infrastructure.Contracts.Persistence.Commons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Infrastructure.Contracts.Persistence.Concrete
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        private readonly AppDbContext _dbContext;
        public MessageRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MessageDto>> GetMessages(int userId)
        {
            var result = from u in _dbContext.Users
                         join m in _dbContext.Messages
                         on u.Email equals m.To
                         where u.Id == userId
                         select new MessageDto { Id=m.ID, Content = m.Content, From = m.From, Read = m.Read };
            return result.ToList();
        }
    }
}
