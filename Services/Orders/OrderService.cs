using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntites;
using Domain.Exceptions;
using Services.Abstraction.Order;
using Services.Specifications;
using Shared.Dtos.Order;

namespace Services.Order
{
    public class OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork ) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto ,  string UserEmail)
        {

            var ShipingAddress = mapper.Map<ShippingAddress>(orderRequestDto.ShipingAddress);
            var Basket = await basketRepository.GetBasketAsync(orderRequestDto.BasketId)??
                throw new BasketNotFoundException(orderRequestDto.BasketId);
            var OrderItems=new List<OrderItem>();
            foreach (var item in Basket.BasketItems) 
            {
                var Product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id)?? throw new ProductNotfoundException(item.Id);
                OrderItems.Add(CreateOrderItem(item, Product));


               
            }
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequestDto.DekiveryMethodId) ??
                   throw new DeliveryMethodException(orderRequestDto.DekiveryMethodId);

            var subTotal = OrderItems.Sum(O => O.Price * O.Quantity);


            var Order = new OrderEntity(UserEmail, ShipingAddress, OrderItems, DeliveryMethod, subTotal,Basket.PaymentIntentId);
            await unitOfWork.GetRepository<OrderEntity, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
          return  mapper.Map<OrderResultDto>(Order);


        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem(new ProductInOrderItem( product.Id,product.Name,product.PictureUrl),item.Quantity,item.Price);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var Order =await unitOfWork.GetRepository<OrderEntity,Guid>().GetAsync(new OrderWithIncludeSpecifications(id))??
                throw new OrderNotFoundExceptions(id);
            return mapper.Map<OrderResultDto>(Order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email)
        {
            var Orders = await unitOfWork.GetRepository<OrderEntity, Guid>().GetAsync(new OrderWithIncludeSpecifications(email));
            return mapper.Map<IEnumerable< OrderResultDto>>(Orders);
        }
    }
}
