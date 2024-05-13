namespace StoredProcedures.Domain.Contracts;

public record CreateBookRequest(string Name, string Category, int AuthorId);