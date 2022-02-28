using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PaymentService.Application.Settings;

namespace PaymentService.Infrastructure.Contracts.Persistence
{
    public class AppMongoDbContext:DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public AppMongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.MongoDbConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(typeof(T).Name.Trim());
        }
    }
}
