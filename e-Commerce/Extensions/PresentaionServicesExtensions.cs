using Domain.Contracts;
using e_Commerce.Factroies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace e_Commerce.Extensions
{
    public static class PresentaionServicesExtensions
    {
        public static IServiceCollection AddPresentaionServices(this IServiceCollection services) 
        {
            services.AddControllers().AddApplicationPart(typeof(Presentaion.AssemblyRefence).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
          
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactroy.CustomValidationErrorResponse;

            });
            services.AddCors(options => {
                options.AddPolicy("CORSPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost/4200");
                });
            });
            return services;
        }
    }
}
