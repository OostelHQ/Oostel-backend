using Oostel.Application.Modules.UserProfiles.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.UserProfile
{
    public class UploadUserProfileCommandValidator: AbstractValidator<UploadUserProfilePictureCommand>
    {
        public UploadUserProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.File).NotNull().WithMessage("Upload Picture is required");
        }
    }
}
