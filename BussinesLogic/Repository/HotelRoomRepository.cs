using AutoMapper;
using BussinesLogic.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BussinesLogic.Repository;

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
    {
        HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
        hotelRoom.CreatedDate = DateTime.Now;
        hotelRoom.UpdatedBy = "";
        var AddHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
        await _db.SaveChangesAsync();
        return _mapper.Map<HotelRoom, HotelRoomDTO>(AddHotelRoom.Entity);
    }

    public async Task<int> DeleteHotelRoom(int roomId)
    {

        HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
        if (roomDetails != null)
        {
            var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

            _db.HotelRoomImages.RemoveRange(allImages);
            _db.HotelRooms.Remove(roomDetails);
            return await _db.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms()
    {
        try
        {
            IEnumerable<HotelRoomDTO> hotelRoomDTOs =
             _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(
                 _db.HotelRooms.Include(x => x.HotelRoomImages));
            return hotelRoomDTOs;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> GetHotelRoom(int roomId)
    {
        try
        {
            HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>
                (await _db.HotelRooms.Include(x => x.HotelRoomImages).FirstOrDefaultAsync(x => x.Id == roomId));
            return hotelRoom;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    //If unique return null else return the room obj
    public async Task<HotelRoomDTO> IsRoomUnique(string Name, int roomId = 0)
    {
        try
        {
            if (roomId == 0)
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>
                    (await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower()));
                return hotelRoom;
            }
            else
            {
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>
                         (await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == Name.ToLower() && x.Id != roomId));
                return hotelRoom;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
    {
        try
        {
            if (roomId == hotelRoomDTO.Id)
            {
                //valid
                HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                HotelRoom room = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO, roomDetails);
                room.UpdatedBy = "";
                room.UpdatedDate = DateTime.Now;
                var updatedRoom = _db.HotelRooms.Update(room);
                await _db.SaveChangesAsync();
                return _mapper.Map<HotelRoom, HotelRoomDTO>(updatedRoom.Entity);
            }
            else
            {
                //invalid
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}