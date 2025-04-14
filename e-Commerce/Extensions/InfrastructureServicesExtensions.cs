using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using StackExchange.Redis;

namespace e_Commerce.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) 
        {
          
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitilaizer>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>(Services => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

            return services;
        }
    }
}
