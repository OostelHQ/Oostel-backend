﻿using MapsterMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [Authorize(Policy = "LandlordAndAgent")]
        public async Task<ActionResult<APIResponse>> CreateHostel([FromForm]HostelRequest request)
        {
            var hostelRequest = (new CreateHostelCommand()
            {
                Country = request.Country,
                HomeSize = request.HomeSize,
                HostelCategory = request.HostelCategory,
                Rooms = request.Rooms,
                Junction = request.Junction,
                HostelDescription= request.HostelDescription,
                HostelFacilities = request.FacilityName,
                HostelFrontViewPicture = request.HostelFrontViewPicture,
                HostelName = request.HostelName,
                IsAnyRoomVacant = request.IsAnyRoomVacant,
                PriceBudgetRange= request.PriceBudgetRange,
                RulesAndRegulation = request.RuleAndRegulation,
                State = request.State,
                Street = request.Street,
                TotalRoom = request.TotalRoom,
                LandlordId = request.LandlordId,
                VideoUrl = request.VideoUrl
            });

            return HandleResult(await Mediator.Send(hostelRequest));
        }

        [HttpPut]
        [Route(HostelRoute.UpdateHostel)]
        [Authorize(Policy = "LandlordAndAgent")]
        public async Task<ActionResult<APIResponse>> UpdateHostel([FromForm] HostelRequest request)
        {
            var hostelRequest = _mapper.Map<UpdateHostelCommand>(request);
            return HandleResult(await Mediator.Send(hostelRequest));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllHostels)]
        [ResponseCache(Duration = 120)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetAllHostels([FromQuery] HostelTypesParam? hostelTypesParam)
        {
            return HandlePagedResult(await Mediator.Send(new GetAllHostelsRequest{hostelTypesParam = hostelTypesParam}));
        }

        [HttpGet]
        [Route(HostelRoute.GetHostelById)]
        [ResponseCache(Duration = 60)]
        [AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetHostelById(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetHostelByIdRequest { HostelId = hostelId}));
        }

        [HttpPost]
        [Route(HostelRoute.CreateRoomForHostel)]
        [Authorize(Policy = "LandlordAndAgent")]
        public async Task<ActionResult<APIResponse>> CreateRoomForHostel([FromForm] RoomRequest request)
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
            
        }

        [HttpPost]
        [Route(HostelRoute.CreateRoomCollections)]
        //[AllowAnonymous]
        [Authorize(Policy = "LandlordAndAgent")]
        public async Task<ActionResult<APIResponse>> CreateRoomCollections([FromForm] RoomCollectionsRequest request)
        {
            var roomRequest = (new CreateRoomCollectionCommand
            {
                HostelId = request.HostelId,
                LandlordId = request.LandlordId,
                roomToCreates = request.roomToCreates
            });

            return HandleResult(await Mediator.Send(roomRequest));

        }

        [HttpPut]
        [Route(HostelRoute.UpdateRoom)]
        [Authorize(Policy = "LandlordAndAgent")]
        public async Task<ActionResult<APIResponse>> UpdateRoom([FromForm] RoomRequest request)
        {
            var roomRequest = _mapper.Map<UpdateRoomCommand>(request);
            return HandleResult(await Mediator.Send(roomRequest));
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        [Route(HostelRoute.GetARoomForHostel)]
        public async Task<ActionResult<APIResponse>> GetARoomForHostel(string hostelId, string roomId)
        {
            return HandleResult(await Mediator.Send(new GetARoomForHostelRequest { HostelId = hostelId, RoomId = roomId}));
        }

        [HttpGet]
        [Route(HostelRoute.GetAllRoomsForHostel)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetAllRoomsForHostel(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetAllRoomsForHostelRequest { HostelId = hostelId }));
        }

        [HttpGet]
        [Route(HostelRoute.GetMyHostels)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetMyHostels(string landlordId)
        {
            return HandleResult(await Mediator.Send(new GetMyHostelsRequest { LandlordId =  landlordId}));
        }

        [HttpGet]
        [Route(HostelRoute.GetMyLikedHostels)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetMyLikedHostels(string userId)
        {
            return HandleResult(await Mediator.Send(new GetMyLikedHostelsRequest { UserId = userId }));
        }

        [HttpGet]
        [Route(HostelRoute.GetHostelLikedUsers)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<APIResponse>> GetHostelLikedUsers(string hostelId)
        {
            return HandleResult(await Mediator.Send(new GetHostelLikedUsersRequest { HostelId = hostelId }));
        }

        [HttpGet]
        [Route(HostelRoute.GetAvailableRoomsPerHostel)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<int>> GetAvailableRoomsPerHostel(string hostelId)
        {
            int availableRoomsCount = await _unitOfWork.RoomRepository.CountAsync(x => x.IsRented && x.HostelId == hostelId);
            return availableRoomsCount;
        }

        [HttpPost]
        [Route(HostelRoute.AddHostelLikes)]
        public async Task<ActionResult<APIResponse>> AddHostelLikes([FromForm] HostelLikeRequest request)
        {
            var hostelLikeRequest = _mapper.Map<AddHostelLikeCommand>(request);
            return HandleResult(await Mediator.Send(hostelLikeRequest));
        }

        [HttpDelete]
        [Route(HostelRoute.DeleteHostelPicture)]
        public async Task<ActionResult<APIResponse>> DeleteHostelPicture(string userId, string hostelPictureId)
        {
            return HandleResult(await Mediator.Send(new DeleteHostelPictureCommand { UserId = userId, HostelPictureId= hostelPictureId}));
        }
    }
}
