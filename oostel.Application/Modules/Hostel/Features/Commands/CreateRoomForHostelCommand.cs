﻿using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Types;
using Oostel.Domain.Hostel.Entities;
using System.Net;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateRoomForHostelCommand : IRequest<APIResponse>
    {
        public string UserId { get; set; }
        public string HostelId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public List<string> RoomFacilities { get; set; }
        public bool IsRented { get; set; }
        public List<IFormFile> Files { get; set; }

        public sealed class CreateRoomForHostelCommandHandler : IRequestHandler<CreateRoomForHostelCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateRoomForHostelCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(CreateRoomForHostelCommand request, CancellationToken cancellationToken)
            {
                var mapData = new RoomDTO()
                {
                    RoomNumber = request.RoomNumber,
                    Files = request.Files,
                    Duration = request.Duration,
                    HostelId = request.HostelId,
                    IsRented = request.IsRented,
                    Price = request.Price,
                    RoomFacilities = request.RoomFacilities,
                };
                var createroomForHostel = await _hostelService.CreateRoomForHostel(request.UserId,mapData);

                if (!createroomForHostel) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
