using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NoteTakingApp.Application.APIResponse
{
    public class APIRESPONSE<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T? Result { get; set; }


        public static APIRESPONSE<T> SuccessResponse(T result ,string message = "Success",HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new APIRESPONSE<T>()
            {
                IsSuccess = true,
                Message = message,
                Result = result,
                StatusCode = statusCode
            };
        }



        public static APIRESPONSE<T> ErrorResponse(string message = "Error", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new APIRESPONSE<T>()
            {
                IsSuccess = !true,
                Message = message,
                StatusCode = statusCode
            };
        }
    }
}
