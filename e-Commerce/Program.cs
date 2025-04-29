
using Domain.Contracts;
using e_Commerce.Extensions;
using e_Commerce.Factroies;
using e_Commerce.MiddleWares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data;
using Persistance.Repositories;
using Services;
using Services.Abstraction;

namespace e_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //===================Presentaion services==================
            builder.Services.AddPresentaionServices();

            //===================Presentaion services==================




            //===================core services==================
            builder.Services.AddCoreServices(builder.Configuration);

            //===================core services==================


            //===================Infrastructure services==================

            builder.Services.AddInfrastructureServices(builder.Configuration);
            //===================Infrastructure services==================



            var app = builder.Build();
           app.UseCustomMiddleWare();
            
            await app.SeedDbAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseStaticFiles();
                

            }
            app.UseCors("CORSPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

           

        }
    }
}
