using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.Hostel.Services;
using Oostel.Common.Constants;
using Oostel.Common.Enums;
using Oostel.Common.Types;
using System.Net;

namespace Oostel.Application.Modules.Hostel.Features.Commands
{
    public class CreateHostelCommand : IRequest<APIResponse>
    {
        public string LandlordId { get; set; }
        public string HostelName { get; set; }
        public string HostelDescription { get; set; }
        public int TotalRoom { get; set; }
        public decimal HomeSize { get; set; }
        public string Street { get; set; }
        public string Junction { get; set; }
        public HostelCategory HostelCategory { get; set; }
        public string State { get; set; }
        public string PriceBudgetRange { get; set; }
        public string Country { get; set; }
        public IEnumerable<RoomToCreate>? Rooms { get; set; }
        public List<string> RulesAndRegulation { get; set; }
        public IFormFile HostelFrontViewPicture { get; set; }
        public List<string> HostelFacilities { get; set; }
        public bool IsAnyRoomVacant { get; set; }

        public sealed class CreateHostelCommandHandler : IRequestHandler<CreateHostelCommand, APIResponse>
        {
            private readonly IHostelService _hostelService;
            private readonly IMapper _mapper;
            public CreateHostelCommandHandler(IHostelService hostelService, IMapper mapper)
            {
                _hostelService = hostelService;
                _mapper = mapper;
            }
            public async Task<APIResponse> Handle(CreateHostelCommand request, CancellationToken cancellationToken)
            {
                //var mapData = _mapper.Map<HostelDTO>(request);
                var hostelRequest = (new HostelDTO()
                {
                    Country = request.Country,
                    HomeSize = request.HomeSize,
                    HostelCategory = request.HostelCategory,
                    Rooms = _mapper.Map<List<RoomDTO>>(request?.Rooms),
                    Junction = request.Junction,
                    HostelDescription = request.HostelDescription,
                    HostelFacilities = request.HostelFacilities,
                    HostelFrontViewPicture = request.HostelFrontViewPicture,
                    HostelName = request.HostelName,
                    IsAnyRoomVacant = request.IsAnyRoomVacant,
                    PriceBudgetRange = request.PriceBudgetRange,
                    RulesAndRegulation = request.RulesAndRegulation,
                    State = request.State,
                    Street = request.Street,
                    TotalRoom = request.TotalRoom,
                    LandlordId = request.LandlordId
                });
                var createhostel = await _hostelService.CreateHostel(hostelRequest);

                if (!createhostel) return APIResponse.GetFailureMessage(HttpStatusCode.BadRequest, null, ResponseMessages.FailedCreation);

                return APIResponse.GetSuccessMessage(HttpStatusCode.Created, data: null, ResponseMessages.SuccessfulCreation);
            }
        }
    }
}
