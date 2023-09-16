using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserAuthentication
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty().MaximumLength(100).WithMessage("Email Address is required"); ;
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100).WithMessage("First Name is required"); ;    
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100).WithMessage("Last Name is required"); ;
            RuleFor(x => x.Password).NotEmpty().MaximumLength(100).WithMessage("Password is required"); ;
        }   
    }
}
