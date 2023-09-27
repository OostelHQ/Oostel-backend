using Oostel.Application.Modules.Hostel.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.Hostel
{
    public class CommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CommentCommandValidator()
        {
            RuleFor(x => x.HostelId).NotEmpty().WithMessage("Hostel Id is required");
            RuleFor(x => x.UserComment).NotEmpty().WithMessage("You can not send an empty comment");
        }
    }
}
