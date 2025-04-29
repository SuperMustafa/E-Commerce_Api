using Domain.Contracts;
using e_Commerce.MiddleWares;

namespace e_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DbInitialaizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await DbInitialaizer.InitializeAsync();
            await DbInitialaizer.InitialzeIdentityAsync();
        }

        public static void UseCustomMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlopalErrorHandelingMiddleWare>();
        }
    }
}
