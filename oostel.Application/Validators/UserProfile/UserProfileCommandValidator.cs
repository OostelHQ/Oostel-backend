using FluentValidation;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserProfile
{
    public class UserProfileCommandValidator : AbstractValidator<CreateStudentProfileCommand>
    {
        public UserProfileCommandValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(w => w.SchoolLevel).MaximumLength(100); 
            RuleFor(p => p.StateOfOrigin).MaximumLength(100);
            RuleFor(p => p.Hobby).MaximumLength(100);
            RuleFor(a => a.Age);
            RuleFor(g => g.Gender).MaximumLength(20);
            RuleFor(r => r.Religion).MaximumLength(50);


        }
    }
}
