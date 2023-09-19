using Oostel.Application.Modules.Hostel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Services
{
    public interface IHostelService
    {
        Task<bool> CreateHostel(HostelDTO hostelDTO);
        Task<List<HostelsResponse>> GetAllHostels();
        Task<AHostelResponse> GetHostelById(string hostelId);
        Task<List<RoomToReturn>> GetAllRoomsForHostel(string hostelId);
        Task<RoomToReturn> GetARoomForHostel(string hostelId, string roomId);
        Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO);

    }
}
