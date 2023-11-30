using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Exceptions
{
    public class RoomDetailsExceptionResponse : Exception
    {
        public RoomDetailsExceptionResponse(string message, HttpStatusCode statusCode)
        : base($"Error : {statusCode} || Message = {message}")
        {
        }
    }
}
