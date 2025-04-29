using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Product;

namespace Services.MappingProfiles
{
    public class PictureURLResolver(IConfiguration configuration ) : IValueResolver<Product, ProductResultDto, string>

    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.PictureUrl)){
                return string.Empty;
            }
            else
            {
                return $"{configuration["BaseUrl"]}{source.PictureUrl}";
            }
        }
    }
}
