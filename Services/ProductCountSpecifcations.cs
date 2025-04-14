using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services
{
    internal class ProductCountSpecifcations:Specifications<Product>
    {
      

        // use to retrive products includeing navigational properties of it

        public ProductCountSpecifcations(ProductParameterSpecifications parameters) :
            base(Product => (!parameters.BrandId.HasValue || Product.ProductBrandId == parameters.BrandId.Value) &&
                          (!parameters.TypeId.HasValue || Product.TypeId == parameters.TypeId.Value)&&
                          (string.IsNullOrWhiteSpace(parameters.Search) ||Product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())))
                           
        {
           
            }
       
        }
    }

