using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using HotelApp.BLL.DTO;
using HotelApp.Models;

namespace HotelApp.BLL.Infrastructure
{
    public class HotelPLMapperProfile : Profile
    {
        public HotelPLMapperProfile()
        {
            CreateMap<TypeComfortEnumDTO, TypeComfortEnumModel>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
            CreateMap<TypeSizeEnumDTO, TypeSizeEnumModel>().ConvertUsingEnumMapping(p => p.MapByName()).ReverseMap();
            CreateMap<HotelRoomModel, HotelRoomDTO>().ReverseMap();
            CreateMap<FreeHotelRoomModel, FreeHotelRoomDTO>().ReverseMap();
            CreateMap<HotelRoomSeachFilterModel, HotelRoomSeachFilterDTO>().ReverseMap();
            CreateMap<ClientModel, ClientDTO>().ReverseMap();

            CreateMap<PaymentStateEnumModel, PaymentStateEnumDTO>().ConvertUsingEnumMapping(p => p.MapByName().MapValue(PaymentStateEnumModel.Booked,PaymentStateEnumDTO.B)
                                                                                                  .MapValue(PaymentStateEnumModel.Paid,PaymentStateEnumDTO.P)).ReverseMap();
            CreateMap<ActiveOrderModel, ActiveOrderDTO>().ReverseMap();
        }
    }
}