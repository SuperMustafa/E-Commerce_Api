using Services;
using Services.Abstraction;

namespace e_Commerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services) 
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(Services.AssembelyReference).Assembly);
            return services;
        }
    }
}
