﻿
using Domain.Entities.OrderEntites;
using Microsoft.Identity.Client;

namespace Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }  
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItem> OrderItems  { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; } 


    }
}
