namespace StoredProcedures.Domain.Contracts;

public record UpdateBookRequest(int BookId, string Name, string Category, int AuthorId);