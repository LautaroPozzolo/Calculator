using System;
using System.Net;

namespace API.Models.Exceptions
{
    public class CustomResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomResponseException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
