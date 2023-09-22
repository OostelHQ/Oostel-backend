using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.RequestFilters;
using Oostel.Application.UserAccessors;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Domain.UserProfiles.Entities;
using Oostel.Infrastructure.Data;
using Oostel.Infrastructure.Media;
using Oostel.Infrastructure.Repositories;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Oostel.Application.Modules.Hostel.Services
{
    public class HostelService : IHostelService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserAccessor _userAccessor;
        private readonly IMapper _mapper;
        private readonly IMediaUpload _mediaUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGenericRepository<Room, string> _genericRepository;
        public HostelService(UserManager<ApplicationUser> userManager, IMediaUpload mediaUpload, ApplicationDbContext applicationDbContext, UnitOfWork unitOfWork, IGenericRepository<Room, string> genericRepository, IMapper mapper, UserAccessor userAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _applicationDbContext= applicationDbContext;
            _mediaUpload = mediaUpload;
            _genericRepository = genericRepository;
            _userAccessor = userAccessor;
        }


        public async Task<bool> CreateHostel(HostelDTO hostelDTO)
        {
            var user = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id == hostelDTO.UserId, true);
            if (user is null) return false;

            var rooms = new List<Room>();

            var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(
                hostelDTO.UserId,
                hostelDTO.HostelName,
                hostelDTO.HostelDescription,
                hostelDTO.TotalRoom,
                hostelDTO.HomeSize,
                hostelDTO.Street,
                hostelDTO.Junction,
                hostelDTO.HostelCategory.GetEnumDescription(),
                hostelDTO.State,
                hostelDTO.Country,
                hostelDTO.RulesAndRegulation,
                hostelDTO.HostelFacilities,
                hostelDTO.IsAnyRoomVacant,
                rooms);

            await _unitOfWork.HostelRepository.Add(hostel);
            await _unitOfWork.SaveAsync();

          /*  var createdHostel = await _unitOfWork.HostelRepository.GetById(hostel.Id);

            foreach (var roomDto in hostelDTO.Rooms)
            {
                var room = Room.CreateRoomForHostelFactory(
                    roomDto.RoomNumber,
                    roomDto.Price,
                    roomDto.Duration,
                    roomDto.RoomFacilities,
                    roomDto.IsRented,
                    createdHostel.Id,
                    new List<string>());
                rooms.Add(room);
            }

            createdHostel.Rooms = rooms;
            createdHostel.LastModifiedDate = DateTime.UtcNow;
           
            await _unitOfWork.HostelRepository.UpdateAsync(createdHostel);
            await _unitOfWork.SaveAsync();*/

            return true;
        }


        /*public async Task<bool> CreateHostel(HostelDTO hostelDTO)
        {
            var user = await _unitOfWork.UserProfileRepository.FindandInclude(x => x.Id ==hostelDTO.UserId, true);
            if (user is null) return false;

            var rooms = new List<Room>();

            foreach (var room in hostelDTO.Rooms)
            {
                var roomToCreate = Room.CreateRoomForHostelFactory(room.RoomNumber, room.Price, room.Duration, room.RoomFacilities, room.IsRented, hostel.Id);
                rooms.Add(roomToCreate);
            }

            var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(hostelDTO.UserId, hostelDTO.HostelName, hostelDTO.HostelDescription,
                                                hostelDTO.TotalRoom, hostelDTO.HomeSize, hostelDTO.Street, hostelDTO.Junction, hostelDTO.HostelCategory.GetEnumDescription(), hostelDTO.State,
                                                hostelDTO.Country, hostelDTO.RulesAndRegulation, hostelDTO.HostelFacilities, hostelDTO.IsAnyRoomVacant,
                                                rooms);
            
                 await _unitOfWork.HostelRepository.Add(hostel);
                await _unitOfWork.SaveAsync();
           

            return true;
        }
        */
        public async Task<bool> UpdateHostel(string hostelId, HostelDTO hostelDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(hostelDTO.UserId);
            if (user is null) return false;

            var existinghostel = await _unitOfWork.HostelRepository.Find(h => h.Id == hostelId);
            if (existinghostel is null) return false;
           // var rooms = _mapper.Map<ICollection<Room>>(hostelDTO.Rooms);

            existinghostel.HostelName = hostelDTO.HostelName;
            existinghostel.HostelDescription = hostelDTO.HostelDescription;
            existinghostel.TotalRoom = hostelDTO.TotalRoom;
            existinghostel.HomeSize = hostelDTO.HomeSize;
            existinghostel.Street = hostelDTO.Street;
            existinghostel.Junction = hostelDTO.Junction;
            existinghostel.HostelCategory = hostelDTO.HostelCategory.GetEnumDescription();
            existinghostel.State = hostelDTO.State;
            existinghostel.Country = hostelDTO.Country;
            existinghostel.RulesAndRegulation = hostelDTO.RulesAndRegulation;
            existinghostel.HostelFacilities = hostelDTO.HostelFacilities;
            existinghostel.IsAnyRoomVacant = hostelDTO.IsAnyRoomVacant;
           // existinghostel.Rooms = rooms;
            existinghostel.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.HostelRepository.UpdateAsync(existinghostel);
            await _unitOfWork.SaveAsync();

            return true;
        }
           
        public async Task<ResultResponse<PagedList<HostelsResponse>>> GetAllHostels(HostelTypesParam hostelTypesParam)
        {

           var hostelsQuery = _applicationDbContext.Hostels
                .Include(r => r.Rooms)
                .OrderBy(d => d.CreatedDate)
                .Select(h => new HostelsResponse {
                    UserId = h.UserId,
                    HostelId = h.Id,
                    HostelCategory = h.HostelCategory,
                    Country = h.Country,
                    HomeSize = h.HomeSize,
                    HostelDescription = h.HostelDescription,
                    HostelFacilities = h.HostelFacilities,
                    PriceBudgetRange = h.PriceBudgetRange,
                    NumberOfRoomsLeft = h.Rooms.Count(x => x.IsRented && x.HostelId == h.Id),
                    Junction = h.Junction,
                    RulesAndRegulation = h.RulesAndRegulation,
                    State = h.State,
                    Street = h.Street,
                    TotalRoom = h.TotalRoom,
                    HostelName = h.HostelName,
                })
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(hostelTypesParam.SearchTerm))
            {
                hostelsQuery = hostelsQuery.Search(hostelTypesParam.SearchTerm);
            }
            else if (hostelTypesParam.HostelCategory != null)
            {
                hostelsQuery = hostelsQuery.Where(o => o.HostelCategory == hostelTypesParam.HostelCategory.GetEnumDescription());
            }
            else if (hostelTypesParam.PriceBudgetRange != null)
            {
                hostelsQuery = hostelsQuery.Where(r => r.PriceBudgetRange == hostelTypesParam.PriceBudgetRange);
            }



            if (hostelsQuery is null)
            {   
                return ResultResponse<PagedList<HostelsResponse>>.Failure(ResponseMessages.NotFound);
            }

            return ResultResponse<PagedList<HostelsResponse>>.Success(await PagedList<HostelsResponse>.CreateAsync(hostelsQuery, hostelTypesParam.PageNumber, hostelTypesParam.PageSize));
        }

        public async Task<AHostelResponse> GetHostelById(string hostelId)
        {
            var hostel =  _unitOfWork.HostelRepository.FindandInclude(x => x.Id == hostelId, true).Result.SingleOrDefault();

            if (hostel is not null)
            {

               var rooms = _mapper.Map<List<RoomToReturn>>(hostel.Rooms);
                var hostelDto = new AHostelResponse()
                {
                    HostelDescription = hostel.HostelDescription,
                    State = hostel.State,
                    Country = hostel.Country,
                    HomeSize = hostel.HomeSize,
                    HostelCategory = hostel.HostelCategory,
                    HostelFacilities = hostel.HostelFacilities,
                    HostelName = hostel.HostelName,
                    Junction = hostel.Junction,
                    Rooms = rooms,
                    RulesAndRegulation = hostel.RulesAndRegulation,
                    Street = hostel.Street,
                    TotalRoom = hostel.TotalRoom,
                    UserId = hostel.UserId
                };
                return hostelDto;
            }
            
            return null;          
        }
          
        public async Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return false;

            var hostel =  await _unitOfWork.HostelRepository.FindandInclude(x => x.Id == roomDTO.HostelId, true); 
            if (hostel is null) return false;

                var room = Room.CreateRoomForHostelFactory(roomDTO.RoomNumber, roomDTO.Price, roomDTO.Duration,
                                                          roomDTO.RoomFacilities, roomDTO.IsRented, roomDTO.HostelId, new List<string>());
            var photoUploadResults = await _mediaUpload.UploadPhotos(roomDTO.Files);
            room.RoomPictures?.AddRange(photoUploadResults.Select(result => result.Url));

            await _unitOfWork.RoomRepository.Add(room);
                await _unitOfWork.SaveAsync();
            
                return true;
        }   

        public async Task<bool> UpdateARoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return false;

            var hostel = await GetHostelById(roomDTO.HostelId);
            if (hostel is null) return false;

            var existingRoom = await _unitOfWork.RoomRepository.Find(h => h.Id == roomDTO.HostelId);
            if (existingRoom is null) return false;

            existingRoom.RoomNumber = roomDTO.RoomNumber;
            existingRoom.Price = roomDTO.Price;
            existingRoom.Duration = roomDTO.Duration;
            existingRoom.RoomFacilities = roomDTO.RoomFacilities; ;
            existingRoom.IsRented = roomDTO.IsRented;
            existingRoom.LastModifiedDate = DateTime.UtcNow;
            await _unitOfWork.RoomRepository.UpdateAsync(existingRoom);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<RoomToReturn> GetARoomForHostel(string hostelId, string roomId)
        {
            var checkIfHostelExist = await GetHostelById(hostelId);
            if (checkIfHostelExist is null)
            {
                return null;
            }
            var room = await _unitOfWork.RoomRepository.FindByCondition(h => h.HostelId.Equals(hostelId) && h.Id.Equals(roomId), false).SingleOrDefaultAsync();

            if (room is null)
            {
                return null;
            }

            var roomDto = _mapper.Map<RoomToReturn>(room);
            return roomDto;
        }

        public async Task<List<RoomToReturn>> GetAllRoomsForHostel(string hostelId)
        {
            var hostelRooms = await _unitOfWork.RoomRepository.FindByCondition(x => x.HostelId == hostelId, true).ToListAsync();

            if (hostelRooms is null)
            {
                return null;
            }

            var roomsDto = _mapper.Map<List<RoomToReturn>>(hostelRooms);
            return roomsDto;
        }

        private async Task<T> CheckForNull<T>(T entity)
        {
            if (entity is null)
                return entity;

            return entity;
        }

    }
}
