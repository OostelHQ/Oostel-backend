using FluentValidation;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserProfile
{
    public class UserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
    {
        public UserProfileCommandValidator()
        {
            RuleFor(w => w.SchoolLevel).MaximumLength(100); 
            RuleFor(p => p.StateOfOrigin).MaximumLength(100);
            RuleFor(p => p.Hobby).MaximumLength(100);

        }
    }
}
