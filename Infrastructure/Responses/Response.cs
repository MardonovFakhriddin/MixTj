using System.Net;

namespace Infrastructure.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }

    public Response(T date)
    {
        Data = date;
        StatusCode = 200;
    }

    public Response(HttpStatusCode statusCode, string message)
    {
        StatusCode = (int)statusCode;
        Message = message;
        Data = default;
    }
}