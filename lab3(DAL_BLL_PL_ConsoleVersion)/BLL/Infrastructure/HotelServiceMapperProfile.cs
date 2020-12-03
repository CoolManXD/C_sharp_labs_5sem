using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public class HotelServiceMapperProfile: Profile
    {
        public HotelServiceMapperProfile()
        {
            CreateMap<TypeComfortEnumDTO, TypeComfortEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeComfortEnumDTO.Undefined, default)).ReverseMap();
            CreateMap<TypeSizeEnumDTO, TypeSizeEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeSizeEnumDTO.Undefined, default)).ReverseMap();
            CreateMap<HotelRoom, FreeHotelRoomDTO>()
                .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
                .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size));
            CreateMap<HotelRoom, HotelRoomDTO>()
                .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
                .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size)).ReverseMap();

            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<PaymentStateEnum, PaymentStateEnumDTO>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
            CreateMap<ActiveOrderDTO, ActiveOrder>().ReverseMap();
        }
    }
}


//var config = new MapperConfiguration(cfg =>
//{
//    cfg.CreateMap<TypeComfortEnumDTO, TypeComfortEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeComfortEnumDTO.Undefined, default)).ReverseMap();
//    cfg.CreateMap<TypeSizeEnumDTO, TypeSizeEnum>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(TypeSizeEnumDTO.Undefined, default)).ReverseMap();
//    cfg.CreateMap<HotelRoom, FreeHotelRoomDTO>()
//        .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
//        .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size));
//    cfg.CreateMap<HotelRoom, HotelRoomDTO>()
//        .ForMember(p => p.TypeComfort, p => p.MapFrom(t => t.TypeComfort.Comfort))
//        .ForMember(p => p.TypeSize, p => p.MapFrom(t => t.TypeSize.Size)).ReverseMap();

//    cfg.CreateMap<Client, ClientDTO>().ReverseMap();
//    cfg.CreateMap<PaymentStateEnum, PaymentStateEnumDTO>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
//    cfg.CreateMap<ActiveOrderDTO, ActiveOrder>().ReverseMap();
//});
//Mapper = config.CreateMapper();