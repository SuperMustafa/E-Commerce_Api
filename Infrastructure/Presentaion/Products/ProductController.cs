using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;

namespace Presentaion.Products
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager ServiceManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductParameterSpecifications Parameters)
        {
            var Products = await ServiceManager.ProductService.GetAllProductsAsync(Parameters);
            return Ok(Products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetAllBrands() 
        {
            var Brands = await ServiceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetAllTypes()
        {
            var Types = await ServiceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductBrandDto>> GetProductById(int id)
        {
            var Product = await ServiceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
    }
}
