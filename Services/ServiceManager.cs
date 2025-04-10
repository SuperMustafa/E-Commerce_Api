using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using Services.Abstraction.Products;
using Services.Products;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _ProductService;

        public ServiceManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _ProductService = new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));


        }

        public IProductService ProductService => _ProductService.Value;
    }
}
