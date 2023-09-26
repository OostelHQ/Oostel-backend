using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.UserAccessors
{
    public interface IUserAccessor
    {
        string GetUsername();
        Task<ApplicationUser> CheckIfTheUserExist(string userId);
    }
}
