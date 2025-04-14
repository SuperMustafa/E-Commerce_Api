using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction.Products;
using Services.Specifications;
using Shared;
using Shared.Dtos.Product;

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

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductParameterSpecifications Parameters)
        {
            var AllProducts = await UnitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(Parameters));
            var TotalCount = await UnitOfWork.GetRepository<Product, int>().GetCountAsync(new ProductCountSpecifcations(Parameters));

            var ProductsResult = Mapper.Map<IEnumerable<ProductResultDto>>(AllProducts);
            var result =new PaginatedResult<ProductResultDto> (Parameters.PageSize,Parameters.PageIndex,TotalCount,ProductsResult);
            
            return result;
        }

        public async Task<IEnumerable<ProductTypeDto>> GetAllTypesAsync()
        {
            var AllTypes = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var AllTypesResult = Mapper.Map<IEnumerable<ProductTypeDto>>(AllTypes);
            return AllTypesResult;  
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var Product = await UnitOfWork.GetRepository<Product,int>().GetAsync(new ProductWithBrandAndTypeSpecifications(id));
            
            return Product is null ?throw new ProductNotfoundException(id) : Mapper.Map<ProductResultDto>(Product);
        }
    }
}
