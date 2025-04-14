using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using Services.Abstraction.Basket;
using Services.Abstraction.Products;
using Services.Basket;
using Services.Products;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _ProductService;
        private readonly Lazy<IBasketService> _basketService;


        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepository basketRepository)
        {
            _ProductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));



        }

        public IProductService ProductService => _ProductService.Value;

        public IBasketService basketService => _basketService.Value;
    }
}
