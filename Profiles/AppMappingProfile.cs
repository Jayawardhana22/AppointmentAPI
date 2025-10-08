using AppointmentAPI.DTOs;
using AppointmentAPI.Models;
using AutoMapper;

namespace AppointmentAPI.Profiles;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();

        // Shop mappings
        CreateMap<Shop, ShopDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Shop.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));
    }
}