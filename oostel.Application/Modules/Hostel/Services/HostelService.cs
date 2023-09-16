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

            var hostel = Domain.Hostel.Entities.Hostel.CreateHostelFactory(hostelDTO.UserId, hostelDTO.HostelName, hostelDTO.HostelDescription,
                                                hostelDTO.TotalRoom, hostelDTO.HomeSize, hostelDTO.Street, hostelDTO.Junction, hostelDTO.State,
                                                hostelDTO.Country, hostelDTO.RulesAndRegulation, hostelDTO.HostelFacilities, hostelDTO.IsAnyRoomVacant);

            var checkIfHostelExist = await _unitOfWork.HostelRepository.GetById(hostelDTO.UserId);
            if(checkIfHostelExist is null)
            {
                var createHostel = await _unitOfWork.HostelRepository.Add(hostel);
                await _unitOfWork.SaveAsync();
            }

            return false;
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
    }
}
