using DataAccess.Data;
using Models;

namespace Business.Repository.IRepository;

public interface IHotelImagesRepository
{
    public Task<int> CreateHotelRoomImage(HotelRoomImageDTO imageDTO);
    public Task<int> DeleteHotelRoomImageByImageId(int imageId);
    public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
    public Task<int> DeleteHotelRoomImageByImageUrl(string imgUrl);
    public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId);
    
}
