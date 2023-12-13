namespace MockVideo.Domain.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllListAsync();
    Task AddAsync(T item);
    Task RemoveAsync(T item);
    Task UpdateAsync(T item);
    Task AddRangeAsync(List<T> items);
}