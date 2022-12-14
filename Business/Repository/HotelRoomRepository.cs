using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository;

public class HotelRoomRepository : IHotelRoomRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<HotelRoomDTO> CreateHotelRoom(HotelRoomDTO hotelRoomDTO)
    {
        HotelRoom hotelRoom = _mapper.Map<HotelRoomDTO, HotelRoom>(hotelRoomDTO);
        hotelRoom.CreatedDate = DateTime.Now;
        hotelRoom.CreatedBy = "Dev";
        hotelRoom.UpdatedBy = "";
        var addedHotelRoom = await _db.HotelRooms.AddAsync(hotelRoom);
        await _db.SaveChangesAsync();
        return _mapper.Map<HotelRoom, HotelRoomDTO>(addedHotelRoom.Entity);
    }
 
    public async Task<HotelRoomDTO> UpdateHotelRoom(int roomId, HotelRoomDTO hotelRoomDTO)
    {
        try
        {
            if (roomId == hotelRoomDTO.Id)
            {
                // Valid
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
                //Invalid
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> GetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null)
    {
        try
        {
            HotelRoom dbHotelRoom = await _db.HotelRooms
                .Include(x => x.HotelRoomImages)
                .FirstOrDefaultAsync(x => x.Id == roomId);
            HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(dbHotelRoom);
            return hotelRoom;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<int> DeleteHotelRoom(int roomId)
    {
        var roomDetails = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId);
        if (roomDetails != null)
        {
            var allImages = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
            _db.HotelRoomImages.RemoveRange(allImages);
            _db.HotelRooms.Remove(roomDetails);
            return await _db.SaveChangesAsync();
        }

        return 0;
    }

    public async Task<IEnumerable<HotelRoomDTO>> GetAllHotelRooms(string checkInDate = null, string checkOutDate = null)
    {
        try
        {
            IEnumerable<HotelRoomDTO> HotelRoomDTOs =
                _mapper.Map<IEnumerable<HotelRoom>, IEnumerable<HotelRoomDTO>>(_db.HotelRooms.Include(x => x.HotelRoomImages));

            return HotelRoomDTOs;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<HotelRoomDTO> IsRoomUnique(string name, int roomId = 0)
    {
        try
        {
            if (roomId == 0)
            {
                HotelRoom dbHotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(dbHotelRoom);
                return hotelRoom;
            }
            else
            {
                HotelRoom dbHotelRoom = await _db.HotelRooms.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.Id != roomId);
                HotelRoomDTO hotelRoom = _mapper.Map<HotelRoom, HotelRoomDTO>(dbHotelRoom);
                return hotelRoom;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}