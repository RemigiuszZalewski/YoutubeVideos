using System.Net;
using System.Text.Json;

namespace ExceptionHandler.Models;

public class Error
{
    public Guid ErrorId { get; set; } = Guid.NewGuid();
    public string Message { get; set; } = string.Empty;
    public string ExceptionType { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
    public string? StackTrace { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}