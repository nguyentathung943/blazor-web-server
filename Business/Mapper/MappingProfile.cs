using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HotelRoomDTO, HotelRoom>();
        CreateMap<HotelRoom,HotelRoomDTO>();
        
        CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();
        CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
    }
}
