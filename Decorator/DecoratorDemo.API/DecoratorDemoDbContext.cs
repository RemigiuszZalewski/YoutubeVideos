using DecoratorDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorDemo.API;

public class DecoratorDemoDbContext : DbContext
{
    public DecoratorDemoDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Student> Students { get; set; }

    public async Task SeedStudentsAsync()
    {
        if (await Students.AnyAsync())
            return;

        var random = new Random();
        var students = new List<Student>();

        string[] firstNames = { "John", "Jane", "Michael", "Emily", "David", "Sarah", "Daniel", "Olivia", "James", "Sophia" };
        string[] lastNames = { "Doe", "Smith", "Johnson", "Brown", "Wilson", "Taylor", "Anderson", "Martinez", "Garcia", "Lopez" };
        string[] majors = { "Computer Science", "Mathematics", "Physics", "Chemistry", "Biology", "Psychology", "Engineering", "Economics", "History", "Art" };

        for (int i = 0; i < 5000; i++)
        {
            students.Add(new Student
            {
                Id = Guid.NewGuid(),
                FirstName = firstNames[random.Next(firstNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Age = random.Next(18, 26),
                Major = majors[random.Next(majors.Length)],
                IsFullTime = random.Next(2) == 0,
                CreditHours = random.Next(9, 19)
            });
        }

        await Students.AddRangeAsync(students);
        await SaveChangesAsync();
    }

}