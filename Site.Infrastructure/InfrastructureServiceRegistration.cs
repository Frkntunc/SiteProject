using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Application.Contracts.Persistence.Repositories.BillPayments;
using Site.Application.Contracts.Persistence.Repositories.Bills;
using Site.Application.Contracts.Persistence.Repositories.Commons;
using Site.Application.Contracts.Persistence.Repositories.Messages;
using Site.Infrastructure.Contracts.Persistence;
using Site.Infrastructure.Contracts.Persistence.Commons;
using Site.Infrastructure.Contracts.Persistence.Concrete;

namespace Site.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        //Api.appsettings'deki sql server connection string'ine ulaşmak ve kullanılacak repository'lerin instance belirtmek için extension metod
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IApartmentRepository, ApartmentRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IBillPaymentRepository, BillPaymentReporsitory>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
