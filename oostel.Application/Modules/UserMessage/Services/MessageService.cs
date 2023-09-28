using Oostel.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.Services
{
    public class MessageService: IMessageService
    {
        private readonly UnitOfWork _unitOfWork;
        public MessageService(UnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }


    }
}
