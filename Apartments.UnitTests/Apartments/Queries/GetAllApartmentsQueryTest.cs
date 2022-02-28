using AutoMapper;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Apartments.UnitTests.Mocks;
using Site.Application.Mapping;
using Site.Application.Features.Queries.Apartments.GetAllApartments;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using Site.Application.Models.Apartment;
using Shouldly;

namespace Apartments.UnitTests.Apartments.Queries
{
    public class GetAllApartmentsQueryTest 
    {
        private readonly IMapper _mapper;
        private readonly Mock<IApartmentRepository> _mocks;
        private readonly Mock<IDistributedCache> _distributedCache;

        public GetAllApartmentsQueryTest()
        {
            _mocks = MockApartmentRepository.GetApartmentRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = new Mapper(mapperConfig);

            _distributedCache = MockCache.GetDistributedCache();
        }

        [Fact]
        public async Task WhenGetAllApartments_ShouldReturnListApartmentModel()
        {
            var handler = new GetAllApartmentsQueryHandler(_mocks.Object, _mapper, (IDistributedCache)_distributedCache);

            var result = await handler.Handle(new GetAllApartmentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ApartmentModel>>();
        }
    }
}
