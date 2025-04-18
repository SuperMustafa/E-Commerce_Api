using Services;
using Services.Abstraction;
using Shared.Dtos;

namespace e_Commerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssembelyReference).Assembly);
            services.Configure<JwtOptionsDto>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
