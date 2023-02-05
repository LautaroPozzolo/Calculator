using API.Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace API.Models;

public class ErrorResponse
{
    public ErrorResponse(Exception exception)
    {
        StatusCode = exception is CustomResponseException ? (exception as CustomResponseException).StatusCode : HttpStatusCode.InternalServerError;

        Message = string.IsNullOrWhiteSpace(exception.Message) ? JsonConvert.SerializeObject(exception) : exception.Message;
    }

    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public string ToJsonString()
        => JsonConvert.SerializeObject(this);
}
