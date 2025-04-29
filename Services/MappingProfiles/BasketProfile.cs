
using AutoMapper;
using Domain.Entities;
using Shared.Dtos.Basket;

namespace Services.MappingProfiles
{
    internal class BasketProfile:Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }  
    }
}
