namespace Movies.Contracts.Dtos;

public record MovieDto(int Id, string Title, string Description, DateTime CreateDate, string Category);