using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Shared.Dtos.Product;

namespace Services.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom<PictureURLResolver>());


            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<ProductBrand, ProductBrandDto>();
           
        }
    }
}
