﻿using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.HostelsVM;
using Oostel.API.ViewModels.UserProfilesVM;
using Oostel.Application.Modules.Hostel.Features.Commands;
using Oostel.Application.Modules.Hostel.Features.Queries;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Features.Queries;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Hostel.Entities;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.Controllers
{
    public class HostelsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public HostelsController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route(HostelRoute.CreateHostel)]
        public async Task<ActionResult<APIResponse>> CreateHostel([FromForm]HostelRequest request)
        {
            var hostelRequest = _mapper.Map<CreateHostelCommand>(request);
            return HandleResult(await Mediator.Send(hostelRequest));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllHostels)]
        public async Task<ActionResult<APIResponse>> GetAllHostels([FromQuery] HostelTypesParam hostelTypesParam)
        {
            return HandlePagedResult(await Mediator.Send(new GetAllHostelsRequest{hostelTypesParam = hostelTypesParam}));
        }

        [HttpGet]
        [Route(HostelRoute.GetHostelById)]
        public async Task<ActionResult<APIResponse>> GetHostelById(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetHostelByIdRequest { HostelId = hostelId}));
        }

        [HttpPost]
        [Route(HostelRoute.CreateAndUpdateRoomForHostel)]
        public async Task<ActionResult<APIResponse>> CreateAndUpdateRoomForHostel([FromForm] RoomRequest request)
        {
            var roomRequest = (new CreateRoomForHostelCommand
            {
                HostelId = request.HostelId,
                UserId = request.UserId,
                Duration = request.Duration,
                IsRented = request.IsRented,
                Price = request.Price,
                RoomFacilities = request.RoomFacilities,
                RoomNumber = request.RoomNumber,
                Files = request.Files
            });
            return HandleResult(await Mediator.Send(roomRequest));
            //var roomRequest = _mapper.Map<CreateRoomForHostelCommand>(request);
            //return HandleResult(await Mediator.Send(roomRequest));
        }

        [HttpGet]
        [Route(HostelRoute.GetARoomForHostel)]
        public async Task<ActionResult<APIResponse>> GetARoomForHostel(string hostelId, string roomId)
        {
            return HandleResult(await Mediator.Send(new GetARoomForHostelRequest { HostelId = hostelId, RoomId = roomId}));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllRoomsForHostel)]
        public async Task<ActionResult<APIResponse>> GetAllRoomsForHostel(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetAllRoomsForHostelRequest { HostelId = hostelId }));
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetAvailableRoomsPerHostel(string hostelId)
        {
            int availableRoomsCount = await _unitOfWork.RoomRepository.CountAsync(x => x.IsRented && x.HostelId == hostelId);
            return availableRoomsCount;
        }
    }
}
