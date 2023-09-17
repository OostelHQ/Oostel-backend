using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.UserAccessors;
using Oostel.Domain.Hostel.Entities;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Services
{
    public class HostelService : IHostelService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;
        private readonly UserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public HostelService(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork, IMapper mapper, UserAccessor userAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<bool> CreateHostel(HostelDTO hostelDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(hostelDTO.UserId);
            if (user is null) return false;

            var existinghostel = await _unitOfWork.HostelRepository.GetById(hostelDTO.UserId);
            if(existinghostel is null)
            {
            var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(hostelDTO.UserId, hostelDTO.HostelName, hostelDTO.HostelDescription,
                                                hostelDTO.TotalRoom, hostelDTO.HomeSize, hostelDTO.Street, hostelDTO.Junction, hostelDTO.State,
                                                hostelDTO.Country, hostelDTO.RulesAndRegulation, hostelDTO.HostelFacilities, hostelDTO.IsAnyRoomVacant);
                 await _unitOfWork.HostelRepository.Add(hostel);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                existinghostel.HostelName = hostelDTO.HostelName;
                existinghostel.HostelDescription = hostelDTO.HostelDescription;
                existinghostel.TotalRoom = hostelDTO.TotalRoom;
                existinghostel.HomeSize = hostelDTO.HomeSize;
                existinghostel.Street= hostelDTO.Street;
                existinghostel.Junction = hostelDTO.Junction;
                existinghostel.State = hostelDTO.State;
                existinghostel.Country= hostelDTO.Country;
                existinghostel.RulesAndRegulation = hostelDTO.RulesAndRegulation;
                existinghostel.HostelFacilities = hostelDTO.HostelFacilities;
                existinghostel.IsAnyRoomVacant = hostelDTO.IsAnyRoomVacant;
                await _unitOfWork.HostelRepository.UpdateAsync(existinghostel);
                await _unitOfWork.SaveAsync();

            }

            return true;
        }

        public async Task<List<HostelDTO>> GetAllHostels()
        {
            var hostel = await _unitOfWork.HostelRepository.GetAll(true);

            var hostelsDto = _mapper.Map<List<HostelDTO>>(hostel);
            return hostelsDto;
        }

        public async Task<HostelDTO> GetHostelById(string hostelId)
        {
            var hostel = await _unitOfWork.HostelRepository.GetById(hostelId);

            var hostelDto = _mapper.Map<HostelDTO>(hostel);
            return hostelDto;
        }

        public async Task<bool> CreateRoomForHostel(string userId, RoomDTO roomDTO)
        {
            var user = await _userAccessor.CheckIfTheUserExist(userId);
            if (user is null) return false;

            var hostel = await GetHostelById(roomDTO.HostelId);
            if (hostel is null) return false;

            var existingRoom = await _unitOfWork.RoomRepository.GetById(roomDTO.HostelId);
            if(existingRoom is null)
            {
                var room = Room.CreateRoomForHostelFactory(roomDTO.RoomNumber, roomDTO.Price, roomDTO.Duration,
                                                          roomDTO.RoomFacilities, roomDTO.RoomCategory, roomDTO.IsRented, roomDTO.HostelId);
                await _unitOfWork.RoomRepository.Add(room);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                existingRoom.RoomNumber = roomDTO.RoomNumber;
                existingRoom.Price = roomDTO.Price;
                existingRoom.Duration = roomDTO.Duration;
                existingRoom.RoomCategory = roomDTO.RoomCategory;
                existingRoom.RoomFacilities = roomDTO.RoomFacilities;
                existingRoom.RoomCategory = roomDTO.RoomCategory;
                existingRoom.IsRented = roomDTO.IsRented;

                await _unitOfWork.RoomRepository.UpdateAsync(existingRoom);
                await _unitOfWork.SaveAsync();
            }

            return true;
        }

        public async Task<RoomDTO> GetAllHostels(string hostelId, string roomId)
        {
            var room = await _unitOfWork.RoomRepository.FindByCondition(h => h.HostelId.Equals(hostelId) && h.Id == roomId);

            var roomDto = _mapper.Map<RoomDTO>(room);
            return roomDto;
        }

        public async Task<List<RoomDTO>> GetHostelRoomsById(string hostelId)
        {
            var hostelRooms = await _unitOfWork.RoomRepository.GetById(hostelId);

            var roomsDto = _mapper.Map<List<RoomDTO>>(hostelRooms);
            return roomsDto;
        }

    }
}
