using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Services.Abstraction.Authentecation;
using Services.Abstraction.Basket;
using Services.Abstraction.Products;
using Services.Authentication;
using Services.Basket;
using Services.Products;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _ProductService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _AuthenticationService;



        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepository basketRepository,UserManager<User> userManager)
        {
            _ProductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _AuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager));




        }

        public IProductService ProductService => _ProductService.Value;

        public IBasketService basketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _AuthenticationService.Value;
    }
}
