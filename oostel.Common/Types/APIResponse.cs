using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Types
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public object Data { get; set; }

        public static APIResponse GetFailureMessage(HttpStatusCode statusCode, object data, string msg)
        {
            var failedResponse = new APIResponse()
            {
                StatusCode = statusCode,
                Data = data,
                Message = msg,
                IsSuccessful = false
            };

            return failedResponse;
        }

        public static APIResponse GetSuccessMessage(HttpStatusCode statusCode, object data, string msg)
        {
            var successResponse = new APIResponse()
            {
                StatusCode = statusCode,
                Data = data,
                Message = msg,
                IsSuccessful = true
            };

            return successResponse;
        }
    }
}
