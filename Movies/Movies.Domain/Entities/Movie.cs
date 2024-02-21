namespace Movies.Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}