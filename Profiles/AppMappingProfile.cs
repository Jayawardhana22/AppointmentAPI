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
        // Add this line just in case you ever need to create users from DTOs:
        CreateMap<UserDto, User>(); 

        // Shop mappings
        // 1. Database -> Frontend (Read)
        CreateMap<Shop, ShopDto>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.FullName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        // 2. Frontend -> Database (Create/Write) <--- THIS WAS MISSING
        CreateMap<ShopDto, Shop>();

        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Shop.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FullName));
        
        // Add the reverse for appointment creation too, just in case
        CreateMap<AppointmentDto, Appointment>();
    }
}