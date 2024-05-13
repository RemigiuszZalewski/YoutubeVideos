namespace StoredProcedures.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public Author Author { get; set; }
    public int AuthorId { get; set; }
}