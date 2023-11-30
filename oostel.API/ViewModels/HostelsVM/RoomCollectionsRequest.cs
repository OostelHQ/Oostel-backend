using Oostel.Application.Modules.Hostel.DTOs;

namespace Oostel.API.ViewModels.HostelsVM
{
    public record RoomCollectionsRequest
    {
        public string LandlordId { get; set; }
        public string HostelId { get; set; }
        public IEnumerable<RoomToCreate> roomToCreates { get; set; }
    }
}
