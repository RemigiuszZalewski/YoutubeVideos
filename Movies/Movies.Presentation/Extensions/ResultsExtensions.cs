using System.Text.Json;

namespace Movies.Presentation.Extensions;

public static class ResultsExtensions
{
    public static IResult OkPaginationResult(this IResultExtensions resultExtensions, 
        int pageSize, int pageNumber, int totalItems, 
        int totalPages, IEnumerable<object> items)
    {
        ArgumentNullException.ThrowIfNull(resultExtensions);
        return new PaginationResult(pageSize, pageNumber, totalItems, totalPages, items);
    }
}

public class PaginationResult(
    int pageSize,
    int pageNumber,
    int totalItems,
    int totalPages,
    IEnumerable<object> items)
    : IResult
{
    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var header = new
        {
            pageSize,
            pageNumber,
            totalItems,
            totalPages
        };
        
        httpContext.Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(header));
        httpContext.Response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");
        httpContext.Response.StatusCode = StatusCodes.Status200OK;
        await httpContext.Response.WriteAsJsonAsync(items);
    }
}