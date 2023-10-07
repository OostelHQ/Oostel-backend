using Oostel.Application.Modules.Hostel.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.Hostel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.RequestFilters
{
    public static class SearchEngineExtension
    {
        public static IQueryable<HostelsResponse> SearchHostel(this IQueryable<HostelsResponse> hostelDto, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return hostelDto;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return hostelDto.Where(w => w.Street.ToLower().Contains(lowerCaseTerm));

        }

        public static IQueryable<GetStudentProfileDTO> SearchStudent(this IQueryable<GetStudentProfileDTO> getStudents, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return getStudents;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return getStudents.Where(w => w.Area.ToLower().Contains(lowerCaseTerm));

        }
    }
}
