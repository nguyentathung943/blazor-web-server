using Business.Repository.IRepository;
using DataAccess.Data;
using Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Business.Repository;

public class AmenityRepository: IAmenityRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AmenityRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<HotelAmenityDTO> CreateHotelAmenity(HotelAmenityDTO hotelAmenityDTO)
    {
        var hotelAmenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenityDTO);
        hotelAmenity.CreatedBy = "";
        hotelAmenity.UpdatedBy = "";
        hotelAmenity.CreatedDate = DateTime.UtcNow;
        var addedAmenity = await _db.HotelAmenities.AddAsync(hotelAmenity);
        await _db.SaveChangesAsync();
        return _mapper.Map<HotelAmenity, HotelAmenityDTO>(addedAmenity.Entity);
    }

    public async Task<HotelAmenityDTO> UpdateHotelAmenity(int amenityId, HotelAmenityDTO hotelAmenity)
    {
        try
        {
            if (amenityId == hotelAmenity.Id)
            {
                var amenityDb = await _db.HotelAmenities.FindAsync(amenityId);
                var amenity = _mapper.Map<HotelAmenityDTO, HotelAmenity>(hotelAmenity, amenityDb);
                amenity.UpdatedBy = "Dev";
                amenity.UpdatedDate = DateTime.Now;
                var updatedAmenity = _db.HotelAmenities.Update(amenity);
                return _mapper.Map<HotelAmenity, HotelAmenityDTO>(updatedAmenity.Entity);
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex) 
        {
            return null;
        }
    }

    public async Task<int> DeleteHotelAmenity(int amenityId, string userId)
    {
        var amenityDetails = await _db.HotelAmenities.FindAsync(amenityId);
        if (amenityDetails != null)
        {
            _db.HotelAmenities.Remove(amenityDetails);
            return await _db.SaveChangesAsync();
        }
        return 0;
    }
    
    public async Task<IEnumerable<HotelAmenityDTO>> GetAllHotelAmenity()
    {
        var amenities = await _db.HotelAmenities.ToListAsync();
        return _mapper.Map<IEnumerable<HotelAmenity>, IEnumerable<HotelAmenityDTO>>(amenities);
    }

    public async Task<HotelAmenityDTO> GetHotelAmenity(int amenityId)
    {
        var amenityDb = await _db.HotelAmenities.FindAsync(amenityId);
        return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityDb);
    }

    public async Task<HotelAmenityDTO> IsSameNameAmenityAlreadyExists(string name)
    {
        try
        {
            var amenityDetails =
                await _db.HotelAmenities.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim()
                );
            return _mapper.Map<HotelAmenity, HotelAmenityDTO>(amenityDetails);
        }
        catch (Exception ex)
        {

        }
        return new HotelAmenityDTO();
    }
}