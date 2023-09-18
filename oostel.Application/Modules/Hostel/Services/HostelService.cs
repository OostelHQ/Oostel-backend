using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.UserAccessors;
using Oostel.Common.Helpers;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Services
{
    public class HostelService : IHostelService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserAccessor _userAccessor;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Room, string> _genericRepository;
        public HostelService(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork, IGenericRepository<Room, string> genericRepository, IMapper mapper, UserAccessor userAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genericRepository = genericRepository;
            _userAccessor = userAccessor;
        }

        public async Task<bool> CreateHostel(HostelDTO hostelDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(hostelDTO.UserId);
            if (user is null) return false;

            var existinghostel = await _unitOfWork.HostelRepository.GetById(hostelDTO.UserId);
            if(existinghostel is null)              
            {
                var rooms = _mapper.Map<ICollection<Room>>(hostelDTO.Rooms);
                var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(hostelDTO.UserId, hostelDTO.HostelName, hostelDTO.HostelDescription,
                                                hostelDTO.TotalRoom, hostelDTO.HomeSize, hostelDTO.Street, hostelDTO.Junction, hostelDTO.HostelCategory.GetEnumDescription(), hostelDTO.State,
                                                hostelDTO.Country, hostelDTO.RulesAndRegulation, hostelDTO.HostelFacilities, hostelDTO.IsAnyRoomVacant,
                                                rooms);
                 await _unitOfWork.HostelRepository.Add(hostel);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateHostel(string hostelId, HostelDTO hostelDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(hostelDTO.UserId);
            if (user is null) return false;

            var existinghostel = await _unitOfWork.HostelRepository.Find(h => h.Id == hostelId);
            if (existinghostel is null) return false;

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
            await _unitOfWork.HostelRepository.UpdateAsync(existinghostel);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<List<HostelsResponse>> GetAllHostels()
        {
            var hostel = await _unitOfWork.HostelRepository.GetAll(false);

          // int availableRoomsCount = await _unitOfWork.RoomRepository.CountAsync(x => !x.IsRented && x.HostelId == Id);

            var hostelsDto = hostel.Select(h => new HostelsResponse
            {
                UserId = h.UserId,
                HostelId = h.Id,
                HostelCategory = h.HostelCategory,
                Country = h.Country,
                HomeSize = h.HomeSize,
                HostelDescription = h.HostelDescription,
                HostelFacilities = h.HostelFacilities,
                NumberOfRoomsLeft = 2, //_unitOfWork.RoomRepository.CountAsync(x => !x.IsRented && x.HostelId == h.Id), //_genericRepository.GetNumberOfAvailableRooms(),
                Junction = h.Junction,
                RulesAndRegulation = h.RulesAndRegulation,
                State = h.State,
                Street = h.Street,
                TotalRoom = h.TotalRoom,
                HostelName = h.HostelName,
            }).ToList();
            //var hostelsDto = _mapper.Map<List<HostelsResponse>>(hostel);
           // var hostels = await Task.WhenAll(hostelsDto);

            return hostelsDto;
        }
        


       /* public async Task<List<HostelsResponse>> GetAllHostels()
        {
            // Retrieve a list of hostels (you may have a different method to do this)
            var hostels = await _unitOfWork.HostelRepository.GetAll(true);

            // Create a list to store the HostelsResponse objects
            var hostelsDto = new List<HostelsResponse>();

            foreach (var hostel in hostels)
            {
                // Count the available rooms for each hostel
                int availableRoomsCount = await _unitOfWork.RoomRepository.CountAsync(x => !x.IsRented && x.HostelId == hostel.Id);

                // Create the HostelsResponse object with the count
                var hostelResponse = new HostelsResponse
                {
                    UserId = hostel.UserId,
                    HostelId = hostel.Id,
                    HostelCategory = hostel.HostelCategory,
                    Country = hostel.Country,
                    HomeSize = hostel.HomeSize,
                    HostelDescription = hostel.HostelDescription,
                    HostelFacilities = hostel.HostelFacilities,
                    NumberOfRoomsLeft = availableRoomsCount,
                    Junction = hostel.Junction,
                    RulesAndRegulation = hostel.RulesAndRegulation,
                    State = hostel.State,
                    Street = hostel.Street,
                    TotalRoom = hostel.TotalRoom,
                    HostelName = hostel.HostelName,
                };

                // Add the HostelsResponse object to the list
                hostelsDto.Add(hostelResponse);
            }

            // Return the list of HostelsResponse objects with counts of available rooms
            return hostelsDto;
        }*/



        public async Task<List<AHostelResponse>> GetHostelById(string hostelId)
        {
            var hostel = await _unitOfWork.HostelRepository.FindandInclude(h => h.Id == hostelId, true);
            if (hostel is null && !hostel.Any())
            {
                return null;
            }

            var hostelDto = _mapper.Map<List<AHostelResponse>>(hostel);
            return hostelDto;
        }

        public async Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return false;

            var hostel = await GetHostelById(roomDTO.HostelId);
            if (hostel is null) return false;

            var existingRoom = await _unitOfWork.RoomRepository.Find(r => r.HostelId == roomDTO.HostelId);
            if(existingRoom is null)
            {
                var room = Room.CreateRoomForHostelFactory(roomDTO.RoomNumber, roomDTO.Price, roomDTO.Duration,
                                                          roomDTO.RoomFacilities, roomDTO.IsRented, roomDTO.HostelId);
                await _unitOfWork.RoomRepository.Add(room);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                existingRoom.RoomNumber = roomDTO.RoomNumber;
                existingRoom.Price = roomDTO.Price;
                existingRoom.Duration = roomDTO.Duration;
                existingRoom.RoomFacilities = roomDTO.RoomFacilities;;
                existingRoom.IsRented = roomDTO.IsRented;

                await _unitOfWork.RoomRepository.UpdateAsync(existingRoom);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }

        public async Task<RoomDTO> GetARoomForHostel(string hostelId, string roomId)
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

            var roomDto = _mapper.Map<RoomDTO>(room);
            return roomDto;
        }

        public async Task<List<RoomDTO>> GetAllRoomsForHostel(string hostelId)
        {
            var hostelRooms = await _unitOfWork.RoomRepository.GetById(hostelId);

            if (hostelRooms is null)
            {
                return null;
            }

            var roomsDto = _mapper.Map<List<RoomDTO>>(hostelRooms);
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
