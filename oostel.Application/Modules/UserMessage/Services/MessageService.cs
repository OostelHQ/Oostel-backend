using Microsoft.AspNetCore.Identity;
using Oostel.Application.Modules.UserMessage.DTOs;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.UserAccessors;
using Oostel.Common.Helpers;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Domain.UserAuthentication.Entities;
using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Services
{
    public class MessageService: IMessageService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public MessageService(UnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork= unitOfWork;
            _userManager= userManager;
        }

        }
    }
}
