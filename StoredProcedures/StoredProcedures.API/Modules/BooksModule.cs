using Microsoft.EntityFrameworkCore;
using StoredProcedures.Domain.Contracts;
using StoredProcedures.Domain.Entities;
using StoredProcedures.Infrastructure;

namespace StoredProcedures.API.Modules;

public static class BooksModule
{
    public static void AddBooksEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/books", async (CreateBookRequest createBookRequest,
            DemoDbContext dbContext) =>
        {
            var book = new Book
            {
                Name = createBookRequest.Name,
                Category = createBookRequest.Category,
                AuthorId = createBookRequest.AuthorId
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return Results.Ok();
        });

        app.MapPut("/books", async (UpdateBookRequest updateBookRequest,
            DemoDbContext dbContext) =>
        {
            var book = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == updateBookRequest.BookId);

            if (book != null)
            {
                book.Name = updateBookRequest.Name;
                book.Category = updateBookRequest.Category;
                book.AuthorId = updateBookRequest.AuthorId;

                await dbContext.SaveChangesAsync();
            }

            return Results.Ok();
        });

        app.MapGet("/get-books", async (DemoDbContext dbContext) =>
        {
            var books = await dbContext.Books.ToListAsync();
            return books;
        });

        app.MapGet("/get-books-by-category-and-authorid",
            async (string category, int authorId, DemoDbContext dbContext) =>
            {
                var books = await dbContext.Books.Where(x => x.Category == category &&
                                                             x.AuthorId == authorId)
                    .ToListAsync();
                return books;
            });
    }
}