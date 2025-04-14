using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction.Basket;
using Services.Abstraction.Products;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
       public  IProductService ProductService { get; }
        public IBasketService basketService { get; }
        
    }
}
