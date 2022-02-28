using Site.Application.Contracts.Persistence.Repositories.Bills;
using Site.Domain.Entities;
using Site.Infrastructure.Contracts.Persistence.Commons;

namespace Site.Infrastructure.Contracts.Persistence.Concrete
{
    public class BillRepository:RepositoryBase<Bill>,IBillRepository
    {
        public BillRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
