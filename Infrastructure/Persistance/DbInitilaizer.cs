using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Persistance.Data;


namespace Persistance
{
    public class DbInitilaizer : IDbInitializer
    {
        private readonly ApplicationDbContext _dbConext;

        public DbInitilaizer(ApplicationDbContext dbConext)
        {
            _dbConext = dbConext;
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
        }
    }
}
