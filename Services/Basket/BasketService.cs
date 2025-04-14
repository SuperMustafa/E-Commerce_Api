

using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction.Basket;
using Shared.Dtos.Basket;

namespace Services.Basket
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService

    {
        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto?>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var UpdatedBasket =  await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basket));
            return UpdatedBasket is null ? throw new Exception("Failed To Update The Basket") : _mapper.Map<BasketDto?>(UpdatedBasket);
        }
    }
}
