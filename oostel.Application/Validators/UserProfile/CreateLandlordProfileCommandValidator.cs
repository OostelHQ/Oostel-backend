using Oostel.Application.Modules.UserProfiles.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserProfile
{
    public class CreateLandlordProfileCommandValidator : AbstractValidator<CreateLandlordProfileCommand>
    {
        public CreateLandlordProfileCommandValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(w => w.Country).MaximumLength(100);
            RuleFor(p => p.StateOfOrigin).MaximumLength(100);
            RuleFor(a => a.Age);
            RuleFor(r => r.Religion).MaximumLength(50);
        }
    }
}
