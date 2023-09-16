using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserAuthentication
{
    public class VerifyUserOTPFromEmailCommandValidator : AbstractValidator<VerifyUserOTPFromEmailCommand>
    {
        public VerifyUserOTPFromEmailCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100).WithMessage("Email Address is required"); ;
            RuleFor(x => x.Otp).NotEmpty().Length(4).WithMessage("OTP is required"); ;
        }
    }
}
