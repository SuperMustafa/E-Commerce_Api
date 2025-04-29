using System.Reflection.Metadata.Ecma335;
using System.Text;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Persistance.Data;
using Persistance.Identity;
using Persistance.Repositories;
using Shared.Dtos;
using StackExchange.Redis;

namespace e_Commerce.Extensions
{
    public static class InfrastructureServicesExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) 
        {
          
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            services.AddDbContext<IdentityStoreContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitilaizer>();
            services.AddScoped<ICashRepository, CashRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>(Services => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.ConfigureIdentityService();
            services.ConfigureJwt(configuration);

            return services;
        }

        public static IServiceCollection ConfigureIdentityService(this IServiceCollection Services)
        {
            Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase =false;
                options.Password.RequireUppercase =false;
                options.Password.RequireNonAlphanumeric =false;
                options.Password.RequiredLength =8;
            }).AddEntityFrameworkStores<IdentityStoreContext>();
            return Services;
        }
        public static IServiceCollection ConfigureJwt(this IServiceCollection Services,IConfiguration configuration)
        {
            var jwtoptions = configuration.GetSection("JwtOptions").Get<JwtOptionsDto>();
            Services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtoptions.Issure,
                    ValidAudience = jwtoptions.Audience,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.SecretKey)),
                };
            });
            Services.AddAuthorization();
            return Services;

        }
        
    }
}
