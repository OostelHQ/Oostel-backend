using FluentValidation;
using Oostel.Application.Modules.Hostel.Features.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Validators.Hostel
{
    public class HostelCommandValidator : AbstractValidator<CreateHostelCommand>
    {
        public HostelCommandValidator() 
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.HostelName).NotEmpty().MaximumLength(100).WithMessage("Hostel Name is required");
            RuleFor(x => x.Street).NotEmpty().MaximumLength(100).WithMessage("Street Name is required");
            RuleFor(x => x.Country).NotEmpty().MaximumLength(100).WithMessage("Country is required");
            RuleFor(x => x.State).NotEmpty().MaximumLength(100).WithMessage("State is required");
            RuleFor(x => x.HomeSize).NotEmpty().WithMessage("Home size is required");
            RuleFor(x => x.HostelDescription).NotEmpty().MaximumLength(500).WithMessage("Hostel Description is required");
            RuleFor(x => x.Junction).NotEmpty().MaximumLength(200).WithMessage("Junction is required");
            RuleFor(x => x.TotalRoom).NotEmpty().WithMessage("Total room is required");
            RuleFor(x => x.IsAnyRoomVacant).NotEmpty().WithMessage("We want to know whelther you have a vacant room");
        }
    }
}
