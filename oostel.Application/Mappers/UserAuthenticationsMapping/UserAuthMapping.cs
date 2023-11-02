using Mapster;
using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Mappers.UserAuthenticationsMapping
{
    public class UserAuthMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, LoginUserCommand>();
           // config.NewConfig<RegisterUserCommand, RegisterUserRequest>();
            config.NewConfig<ApplicationUser, RegisterUserCommand>();
        }
    }
}
