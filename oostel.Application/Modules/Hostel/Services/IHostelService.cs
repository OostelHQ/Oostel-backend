using Oostel.Application.Modules.Hostel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Hostel.Services
{
    public interface IHostelService
    {
        Task<bool> CreateHostel(HostelDTO hostelDTO);
        Task<List<HostelDTO>> GetAllHostels();
        Task<HostelDTO> GetHostelById(string hostelId);

    }
}
