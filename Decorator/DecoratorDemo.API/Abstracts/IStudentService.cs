using DecoratorDemo.API.Models;

namespace DecoratorDemo.API.Abstracts;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetStudentsAsync();
}