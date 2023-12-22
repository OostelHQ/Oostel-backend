using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserRolesProfiles.DTOs
{
    public class GetMyLikedStudentDTO
    {
        public string StudentId { get; set; }
        public string Name{ get; set; }
        public string Gender { get; set; }
        public string Level { get; set; }
        public int LikesCount { get; set; }
        public decimal BudgetAmount { get; set; }
    }
}
