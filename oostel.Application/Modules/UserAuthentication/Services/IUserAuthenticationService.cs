using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserAuthentication.Services
{
    public interface IUserAuthenticationService
    {
        Task<bool> SendVerifyOTPToUserEmail(ApplicationUser user, CancellationToken cancellationToken);

    }
}
