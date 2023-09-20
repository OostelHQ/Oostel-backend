using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Common.Helpers
{
    public class ResultResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public string Error { get; set; }

        public static ResultResponse<T> Success(T value) => new ResultResponse<T> { IsSuccess = true, Data = value };

        public static ResultResponse<T> Failure(string error) => new ResultResponse<T> { IsSuccess = false, Error = error };
    }
}
