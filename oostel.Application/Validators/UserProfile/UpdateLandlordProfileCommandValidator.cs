using Oostel.Application.Modules.UserProfiles.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserProfile
{
    public class UpdateLandlordProfileCommandValidator : AbstractValidator<UpdateLandLordProfileCommand>
    {
        public UpdateLandlordProfileCommandValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(p => p.StateOfOrigin).MaximumLength(100);
            RuleFor(p => p.Country).MaximumLength(100);
            RuleFor(r => r.Religion).MaximumLength(50);
        }
    }
}
