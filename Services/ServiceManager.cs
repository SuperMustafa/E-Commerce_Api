using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Services.Abstraction.Authentecation;
using Services.Abstraction.Basket;
using Services.Abstraction.Order;
using Services.Abstraction.Payment;
using Services.Abstraction.Products;
using Services.Authentication;
using Services.Basket;
using Services.Cash;
using Services.Order;
using Services.Payment;
using Services.Products;
using Shared.Dtos;

namespace Services
{

    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _ProductService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationService> _AuthenticationService;
        private readonly Lazy<IOrderService> _OrderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICashService> _cashService;




        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper, IBasketRepository basketRepository,UserManager<User> userManager,IOptions<JwtOptionsDto> options,IConfiguration configuration,ICashRepository cashRepository)
        {
            _ProductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
            _AuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options,mapper));
            _OrderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepository, unitOfWork, mapper, configuration));
            _cashService = new Lazy<ICashService>(() => new CashService(cashRepository));




        }

        public IProductService ProductService => _ProductService.Value;

        public IBasketService basketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _AuthenticationService.Value;

        public IOrderService OrderService => _OrderService.Value;

        public IPaymentService paymentService => _paymentService.Value;

        public ICashService cashService => _cashService.Value;
    }
}
