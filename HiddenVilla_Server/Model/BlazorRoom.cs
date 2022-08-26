namespace HiddenVilla_Server.Model;
public class BlazorRoom
{
    public int Id { get; set; }
    public string RoomName { get; set; }
    public double Price { get; set; }
    public bool isActive { get; set; }
    
    public List<BlazorRoomProp> RoomProps { get; set; }
}
