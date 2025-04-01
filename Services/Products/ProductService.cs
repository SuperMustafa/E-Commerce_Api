using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction.Products;
using Shared;

namespace Services.Products
{
    internal class ProductService(IUnitOfWork UnitOfWork,IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<ProductBrandDto>> GetAllBrandsAsync()
        {
            var AllBrands = await UnitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var AllBrandsResult = Mapper.Map<IEnumerable<ProductBrandDto>>(AllBrands);
            return AllBrandsResult;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var AllProducts = await UnitOfWork.GetRepository<Product, int>().GetAllAsync();
            var ProductsResult = Mapper.Map<IEnumerable<ProductResultDto>>(AllProducts);
            return ProductsResult;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllTypesAsync()
        {
            var AllTypes = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var AllTypesResult = Mapper.Map<IEnumerable<ProductTypeDto>>(AllTypes);
            return AllTypesResult;  
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var Product = await UnitOfWork.GetRepository<Product,int>().GetAsync(id);
            var ProductResult = Mapper.Map<ProductResultDto>(Product);
            return ProductResult;
        }
    }
}
