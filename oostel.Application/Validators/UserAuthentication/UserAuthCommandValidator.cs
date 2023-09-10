using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserAuthentication
{
    public class UserAuthCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public UserAuthCommandValidator()
        {
            RuleFor(x => x.userRegisterDTO.EmailAddress).EmailAddress().NotEmpty().MaximumLength(100);
            RuleFor(x => x.userRegisterDTO.FirstName).NotEmpty().MaximumLength(100);    
            RuleFor(x => x.userRegisterDTO.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.userRegisterDTO.Password).NotEmpty().MaximumLength(100);
        }
    }
}
