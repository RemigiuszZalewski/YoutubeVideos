using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper.Domain.Entities;

[Table("university_students")]
public class Student
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    [Column("First_name")]
    public string FirstName { get; set; } = string.Empty;
    [Column("Last_name")]
    public string LastName { get; set; } = string.Empty;
    [Column("Email_address")]
    public string EmailAddress { get; set; } = string.Empty;
    [Column("Major")]
    public string Major { get; set; } = string.Empty;
}