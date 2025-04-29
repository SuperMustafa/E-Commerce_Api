using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Abstraction.Authentecation;
using Services.Abstraction.Basket;
using Services.Abstraction.Order;
using Services.Abstraction.Payment;
using Services.Abstraction.Products;

namespace Services.Abstraction
{
    public interface IServiceManager
    {
       public  IProductService ProductService { get; }
        public IBasketService basketService { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IOrderService OrderService { get; }
        public IPaymentService paymentService { get; }
        public ICashService cashService { get; }
         
    }
}
