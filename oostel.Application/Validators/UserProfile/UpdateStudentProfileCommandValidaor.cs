using Oostel.Application.Modules.UserProfiles.Features.Commands;

namespace Oostel.Application.Validators.UserProfile
{
    public class UpdateStudentProfileCommandValidaor : AbstractValidator<UpdateStudentProfileCommand>
    {
        public UpdateStudentProfileCommandValidaor()
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
