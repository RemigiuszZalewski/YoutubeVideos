using Microsoft.EntityFrameworkCore;
using StoredProcedures.Domain.Contracts;
using StoredProcedures.Infrastructure;

namespace StoredProcedures.API.Modules;

public static class BooksStoredProcedureModule
{
    public static void AddBooksStoredProcedureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/stored-procedure-books", async (CreateBookRequest createBookRequest,
            DemoDbContext dbContext) =>
        {
            await dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC CreateBook @name={createBookRequest.Name}, @category={createBookRequest.Category}, @authorId={createBookRequest.AuthorId}");
            return Results.Ok();
        });

        app.MapPut("/stored-procedure-books", async (UpdateBookRequest updateBookRequest,
            DemoDbContext dbContext) =>
        {
            await dbContext.Database.ExecuteSqlInterpolatedAsync($"EXEC UpdateBook @bookId={updateBookRequest.BookId}, @name={updateBookRequest.Name}, @category={updateBookRequest.Category}, @authorId={updateBookRequest.AuthorId}");
            return Results.Ok();
        });

        app.MapGet("/stored-procedure-get-books", async (DemoDbContext dbContext) =>
        {
            var books = await dbContext.Books.FromSqlRaw("EXEC GetBooks").ToListAsync();
            return books;
        });

        app.MapGet("/stored-procedure-get-books-by-category-and-authorid",
            async (string category, int authorId, DemoDbContext dbContext) =>
            {
                var books = await dbContext.Books
                    .FromSqlInterpolated(
                        $"EXEC GetBooksByCategoryAndAuthorId @category={category}, @authorId={authorId}").ToListAsync();
                return books;
            });
    }
}