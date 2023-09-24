using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class UpdateRoomCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string HostelId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public List<string> RoomFacilities { get; set; }
        public bool IsRented { get; set; }
        public List<IFormFile> Files { get; set; }
        
        public sealed class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public UpdateRoomCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
            {
                var mapData = _mapper.Map<RoomDTO>(request);
                var updateroom = await _hostelService.UpdateARoomForHostel(request.UserId, mapData);

                if (!updateroom) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
