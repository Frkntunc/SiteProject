using Apartments.UnitTests.Mocks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shouldly;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Features.Queries.Apartments.GetApartment;
using Site.Application.Mapping;
using Site.Application.Models.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Apartments.UnitTests.Apartments.Queries
{
    public class GetApartmentQueryTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IApartmentRepository> _mocks;
        private readonly Mock<IDistributedCache> _distributedCache;

        public GetApartmentQueryTest()
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
        public async Task WhenGetApartmentWithId_ShouldReturnApartmentModel()
        {
            var handler = new GetApartmentQueryHandler(_mocks.Object, _mapper, (IDistributedCache)_distributedCache);

            int Id = 1;
            var result = await handler.Handle(new GetApartmentQuery(Id), CancellationToken.None);

            result.ShouldBeOfType<ApartmentModel>();
        }
    }
}
