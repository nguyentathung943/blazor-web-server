using System.Text.Json.Serialization;
using HiddenVilla_Client.Service.IService;
using Newtonsoft.Json;
using Models;

namespace HiddenVilla_Client.Service
{
    public class HotelRoomService: IHotelRoomService
    {
        private readonly HttpClient _client;

        public HotelRoomService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<HotelRoomDTO>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var response = await _client.GetAsync($"api/hotelroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await response.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDTO>>(content);
            return rooms;
        }

        public async Task<HotelRoomDTO> GetHotelRoomDetails(int roomId, string checkInDate, string checkOutDate)
        {
            throw new NotImplementedException();
        }
    }
}
