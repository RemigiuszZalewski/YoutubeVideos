namespace DecoratorDemo.API.Models;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Major { get; set; } = string.Empty;
    public bool IsFullTime { get; set; }
    public int CreditHours { get; set; }
}