namespace Movies.Contracts.Requests.Common;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaximumPageSize ? MaximumPageSize : value;
    }
    
    private int _pageSize = 10;
    private const int MaximumPageSize = 100;
}