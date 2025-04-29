using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntites;
using Microsoft.AspNetCore.Identity;
using Persistance.Data;


namespace Persistance
{
    public class DbInitilaizer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbConext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitilaizer(ApplicationDbContext dbConext,UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
            _dbConext = dbConext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            if (_dbConext.Database.GetPendingMigrations().Any()) 
            {
                await _dbConext.Database.MigrateAsync();
            }
            if (_dbConext.ProductBrands.Any() == false)
            {
                var ProductTypesData = await File.ReadAllTextAsync(path:@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);

                if(types is not null && types.Any())
                {
                    await _dbConext.ProductTypes.AddRangeAsync(types);
                    await _dbConext.SaveChangesAsync();
                }


            }

            if (_dbConext.ProductBrands.Any() == false)
            {
                var ProductBrandsData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");
                var types = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);

                if (types is not null && types.Any())
                {
                    await _dbConext.ProductBrands.AddRangeAsync(types);
                    await _dbConext.SaveChangesAsync();
                }


            }
            if (_dbConext.Products.Any() == false)
            {
                var ProductsData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistance\Data\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (products is not null && products.Any())
                {
                    await _dbConext.Products.AddRangeAsync(products);
                    await _dbConext.SaveChangesAsync();
                }


            }

            if (_dbConext.DeliveryMethods.Any() == false)
            {
                var DeliveryMethodsData = await File.ReadAllTextAsync(path: @"..\Infrastructure\Persistance\Data\DataSeeding\delivery.json");
                var Data = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                if (Data is not null && Data.Any())
                {
                    await _dbConext.DeliveryMethods.AddRangeAsync(Data);
                    await _dbConext.SaveChangesAsync();
                }


            }
        }

        public async Task InitialzeIdentityAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            }
            if (!_userManager.Users.Any())
            {
                var SuperAdminUser = new User
                {
                    DisplayName = "Mustafa",
                    Email = "Mustafa@Gmail.com",
                    UserName = "Mustafa",
                    PhoneNumber = "01019540969"
                    
                };
                var AdminUser = new User
                {
                    DisplayName = "Rewan",
                    Email = "Rewan@Gmail.com",
                    UserName = "Rewan",
                    PhoneNumber = "012195555569"

                };
                await _userManager.CreateAsync(SuperAdminUser,"Passw0rd");
                await _userManager.CreateAsync(AdminUser, "Passw0rd");

                //================Set roles=================
                await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(AdminUser, "Admin");




            }
        }
    }
}
