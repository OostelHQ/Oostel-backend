namespace Oostel.API.ViewModels.HostelsVM
{
    public class RoomRequest
    {
        public string UserId { get; set; }
        public string HostelId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public List<string> RoomFacilities { get; set; }
        public bool IsRented { get; set; }
    }
}
