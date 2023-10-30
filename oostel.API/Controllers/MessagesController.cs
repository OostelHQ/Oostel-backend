using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Oostel.API.APIRoutes;
using Oostel.API.Extensions;
using Oostel.Common.Types;
using Oostel.Common.Types.RequestFeatures;
using Oostel.Infrastructure.Repositories;

namespace Oostel.API.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        public MessagesController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper= mapper;
            _unitOfWork= unitOfWork;
        }

        [HttpGet]
        [Route(MessageRoute.GetMessagesForUser)]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            var messages = await _unitOfWork.MessageRepository.GetMessagesForUser(messageParams);
            
            Response.AddPaginationHeader(messages.MetaData.CurrentPage, messages.MetaData.PageSize,
                messages.MetaData.TotalCount, messages.MetaData.TotalPages);

            return Ok(messages);
        }

    }
}
