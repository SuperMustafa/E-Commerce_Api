using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Shared;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {


        // use to retrive product with specific id includeing navigational properties of it
        public ProductWithBrandAndTypeSpecifications(int id) : base(Product => Product.Id == id)
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);

        }

        // use to retrive products includeing navigational properties of it

        public ProductWithBrandAndTypeSpecifications(ProductParameterSpecifications parameters) :
            base(Product=>(!parameters.BrandId.HasValue||Product.ProductBrandId==parameters.BrandId.Value)&&
                          (!parameters.TypeId.HasValue||Product.TypeId==parameters.TypeId.Value)&&
                          (string.IsNullOrWhiteSpace(parameters.Search) ||Product.Name.ToLower().Contains(parameters.Search.ToLower().Trim())) )
                            
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);

            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(Product => Product.Price);
                        break;

                    case ProductSortOptions.PriceAsc:
                        SetOrderBy(Product => Product.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(Product => Product.Name);
                        break;

                        default :
                        SetOrderBy(Product => Product.Name);
                        break;
                            


                }
            }
            ApplyPagenation(parameters.PageIndex, parameters.PageSize);
        }
    }
}
