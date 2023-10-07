using MapsterMapper;
using Oostel.Application.Modules.UserProfiles.DTOs;
using Oostel.Application.Modules.UserProfiles.Features.Commands;
using Oostel.Application.Modules.UserProfiles.Services;
using Oostel.Common.Constants;
using Oostel.Common.Helpers;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserProfiles.Features.Queries
{
    public class GetAllStudentsRequest : IRequest<ResultResponse<PagedList<GetStudentProfileDTO>>>
    {
        public StudentTypeParams StudentTypeParams { get; set; }

        public sealed class GetAllStudentsRequestCommand : IRequestHandler<GetAllStudentsRequest, ResultResponse<PagedList<GetStudentProfileDTO>>>
        {
            private readonly IUserRolesProfilesService _userProfilesService;
            public GetAllStudentsRequestCommand(IUserRolesProfilesService userProfilesService) => 
                _userProfilesService = userProfilesService;

            public async Task<ResultResponse<PagedList<GetStudentProfileDTO>>> Handle(GetAllStudentsRequest request, CancellationToken cancellationToken)
            {
                var studentProfile = await _userProfilesService.GetAllStudents(request.StudentTypeParams);

                if (studentProfile is null) return ResultResponse<PagedList<GetStudentProfileDTO>>.Failure(ResponseMessages.NotFound);

                return ResultResponse<PagedList<GetStudentProfileDTO>>.Success(studentProfile.Data);
            }
        }
    }
}
