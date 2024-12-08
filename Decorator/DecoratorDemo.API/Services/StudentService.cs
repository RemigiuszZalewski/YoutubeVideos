using DecoratorDemo.API.Abstracts;
using DecoratorDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorDemo.API.Services;

public class StudentService : IStudentService
{
    private readonly DecoratorDemoDbContext _decoratorDemoDbContext;

    public StudentService(DecoratorDemoDbContext decoratorDemoDbContext)
    {
        _decoratorDemoDbContext = decoratorDemoDbContext;
    }
    
    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        return await _decoratorDemoDbContext.Students.ToListAsync();
    }
}
