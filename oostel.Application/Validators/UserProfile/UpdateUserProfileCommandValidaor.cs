using Oostel.Application.Modules.UserProfiles.Features.Commands;

namespace Oostel.Application.Validators.UserProfile
{
    public class UpdateUserProfileCommandValidaor : AbstractValidator<UpdateStudentProfileCommand>
    {
        public UpdateUserProfileCommandValidaor()
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
