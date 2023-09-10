using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserProfiles.Entities
{
    public class UserProfile : BaseEntity<string>
    {

        public ApplicationUser User { get; set; }
    }
}
