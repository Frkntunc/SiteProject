using Apartments.UnitTests.Mocks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Features.Commands.Apartments.AddApartment;
using Site.Application.Mapping;
using Site.Domain.Entities;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Threading;

namespace Apartments.UnitTests.Apartments.Commands
{
    public class AddApartmentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IApartmentRepository> _mocks;
        private readonly Mock<IDistributedCache> _distributedCache;

        public AddApartmentCommandHandlerTests()
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
        public async Task WhenAddApartment_Should_ReturnApartment()
        {
            AddApartmentCommand addApartmentCommand = new AddApartmentCommand();
            addApartmentCommand.ApartmentNumber = 1;
            addApartmentCommand.Blok = 2;
            addApartmentCommand.Floor = 3;
            addApartmentCommand.Owner = "Ali";
            addApartmentCommand.Status = "Boş";
            addApartmentCommand.Type = "2+1";
            addApartmentCommand.UserId = 3;

            var handler = new AddApartmentCommandHandler(_mocks.Object, _mapper, (IDistributedCache)_distributedCache);

            var result = await handler.Handle(addApartmentCommand, CancellationToken.None);

            result.ShouldBeOfType<Apartment>();
        }
    }
}
