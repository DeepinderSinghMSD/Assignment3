using Assignment3.Models;
using AutoMapper;

namespace Assignment3
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartDto>();
            CreateMap<CartProduct, CartProductDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<CartProductDto, CartProduct>();

            CreateMap<Order, OrderDtos>()
            .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));
            CreateMap<OrderDtos, Order>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<OrderProductDto, OrderProduct>();
        }
    }
}