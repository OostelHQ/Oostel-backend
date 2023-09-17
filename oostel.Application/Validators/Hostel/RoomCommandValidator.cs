using Oostel.Application.Modules.Hostel.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.Hostel
{
    public class RoomCommandValidator : AbstractValidator<CreateRoomForHostelCommand>
    {
        public RoomCommandValidator() 
        {
            RuleFor(o => o.RoomNumber).NotEmpty().WithMessage("Room Number is required");
            RuleFor(o => o.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(o => o.Duration).NotEmpty().WithMessage("Duration is required");
            RuleFor(o => o.HostelId).NotEmpty().WithMessage("Hostel Id is required");
            RuleFor(o => o.UserId).NotEmpty().WithMessage("User Id is required");
            RuleFor(o => o.IsRented).NotEmpty().WithMessage("Price is required");
            RuleFor(o => o.RoomCategory).NotEmpty().WithMessage("Price is required");
        }
    }
}
