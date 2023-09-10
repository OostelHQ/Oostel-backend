using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.ViewModels.UserAuthenticationsVM;
using Oostel.Application.Modules.UserAuthentication.Features.Commands;
using Oostel.Common.Types;

namespace Oostel.API.Controllers
{
    public class AuthenticationsController : BaseController
    {
        private readonly IMapper _mapper;
        public AuthenticationsController(IMapper mapper) => _mapper = mapper;

        [HttpPost()]
        [Route(UserAuthenticationRoute.RegisterUser)]
        public async Task<ActionResult<APIResponse>> RegisterUser([FromForm] RegisterUserRequest request)
        {
            var registerRequest = _mapper.Map<RegisterUserCommand>(request);
            return HandleResult(await Mediator.Send(registerRequest));
        }
    }
}
