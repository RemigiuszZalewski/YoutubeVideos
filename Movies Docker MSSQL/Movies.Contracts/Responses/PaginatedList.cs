namespace Movies.Contracts.Responses;

public class PaginatedList<T>
{
    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        Items = items;
    }
    
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public IEnumerable<T> Items { get; set; }
}