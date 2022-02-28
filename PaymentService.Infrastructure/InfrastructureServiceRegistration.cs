using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Consumer;
using PaymentService.Application.Contracts.Persistence.Repositories.Commons;
using PaymentService.Application.Contracts.Persistence.Repositories.CreditCards;
using PaymentService.Application.Settings;
using PaymentService.Infrastructure.Contracts.Persistence.Commons;
using PaymentService.Infrastructure.Contracts.Persistence.Concrete;

namespace PaymentService.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoSettings>(options =>
            {
                options.MongoDbConnectionString = configuration.GetSection("MongoDbConnection:MongoDbConnectionString").Value;
                options.Database = configuration.GetSection("MongoDbConnection:Database").Value;
            });

            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
            services.AddTransient<IRabbitMqConsumer, RabbitMqConsumerHandler>();

            return services;
        }
    }
}
