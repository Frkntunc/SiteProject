using Apartments.UnitTests.Mocks;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shouldly;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Features.Commands.Apartments.DeleteApartment;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Apartments.UnitTests.Apartments.Commands
{
    public class DeleteApartmentCommandHandlerTest
    {
        private readonly Mock<IApartmentRepository> _mocks;
        private readonly Mock<IDistributedCache> _distributedCache;

        public DeleteApartmentCommandHandlerTest()
        {
            _mocks = MockApartmentRepository.GetApartmentRepository();

            _distributedCache = MockCache.GetDistributedCache();
        }

        [Fact]
        public async Task WhenDeleteApartment_WithInvalidId_ShouldThrowInvalidOperationException()
        {
            int Id = 8;
            DeleteApartmentCommand deleteApartmentCommand = new DeleteApartmentCommand(Id);

            var handler = new DeleteApartmentCommandHandler(_mocks.Object, (IDistributedCache)_distributedCache);

            var result = await handler.Handle(deleteApartmentCommand, CancellationToken.None);

            result.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
