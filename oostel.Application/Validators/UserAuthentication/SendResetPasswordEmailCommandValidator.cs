using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserAuthentication
{
    public class SendResetPasswordEmailCommandValidator : AbstractValidator<SendResetPasswordEmailCommand>
    {
        public SendResetPasswordEmailCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100).WithMessage("Email is required"); ;
        }
    }
}
