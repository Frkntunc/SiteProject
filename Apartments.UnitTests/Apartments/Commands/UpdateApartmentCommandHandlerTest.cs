using Apartments.UnitTests.Mocks;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shouldly;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Features.Commands.Apartments.UpdateApartment;
using Site.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Apartments.UnitTests.Apartments.Commands
{
    public class UpdateApartmentCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IApartmentRepository> _mocks;
        private readonly Mock<IDistributedCache> _distributedCache;

        public UpdateApartmentCommandHandlerTest()
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
        public async Task WhenUpdateApartment_WithInvalidId_ShoulThrowInvalidOperationException()
        {
            UpdateApartmentCommand updateApartmentCommand = new UpdateApartmentCommand();
            updateApartmentCommand.ApartmentNumber = 1;
            updateApartmentCommand.Blok = 2;
            updateApartmentCommand.Floor = 3;
            updateApartmentCommand.Owner = "Ali";
            updateApartmentCommand.Status = "Boş";
            updateApartmentCommand.Type = "2+1";
            updateApartmentCommand.Id = 8;

            var handler = new UpdateApartmentCommandHandler(_mocks.Object, _mapper, (IDistributedCache)_distributedCache);

            var result = await handler.Handle(updateApartmentCommand, CancellationToken.None);

            result.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
