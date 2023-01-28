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

    }
}
