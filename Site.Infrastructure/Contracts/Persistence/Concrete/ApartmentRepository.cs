using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Domain.Entities;
using Site.Infrastructure.Contracts.Persistence.Commons;

namespace Site.Infrastructure.Contracts.Persistence.Concrete
{
    public class ApartmentRepository:RepositoryBase<Apartment>,IApartmentRepository
    {
        public ApartmentRepository(AppDbContext dbContext):base(dbContext)
        {

        }
    }
}
