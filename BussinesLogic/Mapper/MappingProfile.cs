using AutoMapper;
using DataAccess.Data;
using Models;

namespace BussinesLogic.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<HotelRoomDTO, HotelRoom>();
        CreateMap<HotelRoom, HotelRoomDTO>();

        CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();
        CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();

        CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>().ForMember(x => x.HotelRoomDTO, opt => opt.MapFrom(c => c.HotelRoom));
        CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();
    }
}
