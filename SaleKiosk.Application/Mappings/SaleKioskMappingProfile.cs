using AutoMapper;
using SaleKiosk.Domain.Models;
using SaleKiosk.SharedKernel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.Application.Mappings
{
    public class KioskMappingProfile : Profile
    {
        public KioskMappingProfile()
        {
            CreateMap<Hotel, HotelDto>();
            CreateMap<CreateHotelDto, Hotel>()
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Desc));

            CreateMap<OrderStatusEnum, OrderStatusEnumDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(m => m.Email, c => c.MapFrom(s => s.Email));

            CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}, Email:  {src.User.Email}"))
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));
            CreateMap<CreateReviewDto, Review>();
        }
    }
}
