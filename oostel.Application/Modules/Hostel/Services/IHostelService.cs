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
        Task<string> CreateHostel(HostelDTO hostelDTO);
        Task<ResultResponse<PagedList<HostelsResponse>>> GetAllHostels(HostelTypesParam hostelTypesParam);
        Task<HostelDetailsResponse> GetHostelById(string hostelId);
        Task<List<RoomToReturn>> GetAllRoomsForHostel(string hostelId);
        Task<RoomToReturn> GetARoomForHostel(string hostelId, string roomId);
        Task<bool> AddHostelLike(string sourceId, string hostelLikeId);
        Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO);
        Task<UpdateHostelResponse> UpdateHostel(string hostelId, HostelDTO hostelDTO);
        Task<ResultResponse<CommentDTO>> CreateComment(CreateCommentDTO createCommentDTO);
        Task<RoomUpdateResponse> UpdateARoomForHostel(string userId, RoomDTO roomDTO);
        Task<List<GetMyHostelsDTO>> GetMyHostels(string landlordId);
        Task<ResultResponse<List<CommentDTO>>> GetComments(string hostelId);
        Task<bool> DeleteProductPicture(string userId, string pictureId);
        Task<List<string>> GetMyLikedHostels(string userId);
        Task<HostelLikesAndCount> GetHostelLikedUsersAndCount(string hostelId);
        Task<(IEnumerable<RoomCollectionsDTO> roomDTOs, string ids)> CreateRoomCollectionAsync(string landlordId, string hostelId, IEnumerable<RoomToCreate> roomsToCreates);
    }
}
