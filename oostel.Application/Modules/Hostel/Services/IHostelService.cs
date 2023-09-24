using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
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
        Task<ResultResponse<PagedList<HostelsResponse>>> GetAllHostels(HostelTypesParam hostelTypesParam);
        Task<AHostelResponse> GetHostelById(string hostelId);
        Task<List<RoomToReturn>> GetAllRoomsForHostel(string hostelId);
        Task<RoomToReturn> GetARoomForHostel(string hostelId, string roomId);
        Task<bool> AddHostelLike(string sourceId, string hostelLikeId);
        Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO);

    }
}
