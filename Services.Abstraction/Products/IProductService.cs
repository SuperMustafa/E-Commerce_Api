using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Dtos.Product;

namespace Services.Abstraction.Products
{
    public interface IProductService
    {
        //Get All Products
        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductParameterSpecifications Parameters);
        //Get product by id
        public Task<ProductResultDto?> GetProductByIdAsync(int id);
        //get all Brands
        public Task<IEnumerable<ProductBrandDto>> GetAllBrandsAsync();
        //get all types
        public Task<IEnumerable<ProductTypeDto>> GetAllTypesAsync();



    }
}
