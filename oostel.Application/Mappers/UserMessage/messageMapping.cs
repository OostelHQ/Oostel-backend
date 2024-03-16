using Mapster;
using Oostel.Application.Modules.UserMessage.Features.Commands;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Domain.UserRoleProfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _userMessage = Oostel.Domain.UserMessage.UserMessage;

namespace Oostel.Application.Mappers.UserMessage
{
    public class MessageMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<_userMessage, SendMessageCommand>();
        }

    }
}
