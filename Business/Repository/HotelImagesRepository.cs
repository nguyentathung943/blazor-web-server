using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository;

public class HotelImagesRepository : IHotelImagesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO)
    {
        var image = _mapper.Map<HotelRoomImageDTO, HotelRoomImage>(imageDTO);
        await _db.HotelRoomImages.AddAsync(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByImageId(int imageId)
    {
        var image = await _db.HotelRoomImages.FindAsync(imageId);
        _db.HotelRoomImages.Remove(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
    {
        var images = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
        _db.HotelRoomImages.RemoveRange(images);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByImageUrl(string imgUrl)
    {
        var image = await _db.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imgUrl.ToLower());
        // if (image == null)
        // {
        //     return 0;
        // }
        _db.HotelRoomImages.Remove(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
    {
        var images = await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();
         return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(images);
    }
}