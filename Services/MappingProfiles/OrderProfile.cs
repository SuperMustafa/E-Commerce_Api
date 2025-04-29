using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.OrderEntites;
using Shared.Dtos.Order;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile() 
        
        {
            CreateMap<ShippingAddress, ShipingAddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>() // source to destenation
                .ForMember(O => O.ProductId, P => P.MapFrom(P => P.Product.ProductID))     // destenation to source
                .ForMember(O => O.ProductName, P => P.MapFrom(P => P.Product.ProductName))
                .ForMember(O => O.PictureUrl, P => P.MapFrom(P => P.Product.PictureUrl));

            CreateMap<OrderEntity, OrderResultDto>()
              .ForMember(dest => dest.PaymentStatus,
                opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
             .ForMember(dest => dest.DeliveryMethod,
                opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
             .ForMember(dest => dest.SubTotal,
                opt => opt.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();




        }
    }
}
