using Microsoft.Extensions.Caching.Distributed;
using Moq;

namespace Apartments.UnitTests.Mocks
{
    public static class MockCache
    {
        public static Mock<IDistributedCache> GetDistributedCache()
        {
            
            var mockRepo = new Mock<IDistributedCache>();
            mockRepo.Setup(x => x.Get(It.IsAny<string>()));
            return mockRepo;
        }
    }
}
