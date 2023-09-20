using Oostel.Application.Modules.Hostel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.RequestFilters
{
    public static class SearchEngineExtension
    {
        public static IQueryable<HostelDTO> Search(this IQueryable<HostelDTO> hostelDto, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return hostelDto;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return hostelDto.Where(w => w.Street.ToLower().Contains(lowerCaseTerm));

        }
    }
}
