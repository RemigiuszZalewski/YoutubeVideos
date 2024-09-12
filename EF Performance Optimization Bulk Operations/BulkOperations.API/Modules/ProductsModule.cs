using BulkOperations.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BulkOperations.API.Modules;

public static class ProductsModule
{
    public static void AddProductsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("products-count", async (BulkOperationsDbContext dbContext)
            => Results.Ok(await dbContext.Products.CountAsync()));

        app.MapPut("/update-without-bulk",
            async (string categoryToUpdate, int percent, BulkOperationsDbContext dbContext) =>
            {
                if (string.IsNullOrEmpty(categoryToUpdate))
                {
                    return Results.BadRequest("Category cannot be null/empty");
                }

                if (percent <= 0)
                {
                    return Results.BadRequest("Percent cannot be <= 0");
                }

                decimal priceFactor = 1 + percent / 100.0m;

                List<Product> productsToUpdate = await dbContext.Products
                    .Where(x => x.Category == categoryToUpdate).ToListAsync();

                foreach (var product in productsToUpdate)
                {
                    product.Price *= priceFactor;
                }

                var rowsAffected = await dbContext.SaveChangesAsync();

                return Results.Ok($"Rows affected: {rowsAffected}");
            });
        
        app.MapPut("/update-with-bulk",
            async (string categoryToUpdate, int percent, BulkOperationsDbContext dbContext) =>
            {
                if (string.IsNullOrEmpty(categoryToUpdate))
                {
                    return Results.BadRequest("Category cannot be null/empty");
                }
                
                if (percent <= 0)
                {
                    return Results.BadRequest("Percent cannot be <= 0");
                }
                
                decimal priceFactor = 1 + percent / 100.0m;
                
                int updatedRows = await dbContext.Products
                    .Where(p => p.Category == categoryToUpdate)
                    .ExecuteUpdateAsync(p => p.SetProperty(
                        prod => prod.Price,
                        prod => prod.Price * priceFactor)
                    );

                return Results.Ok($"Rows affected: {updatedRows}");
            });
        
        app.MapDelete("/delete-without-bulk",
            async (string categoryToDelete, BulkOperationsDbContext dbContext) =>
        {
            if (string.IsNullOrEmpty(categoryToDelete))
            {
                return Results.BadRequest("Category cannot be null or empty.");
            }
            
            List<Product> productsToDelete = await dbContext.Products
                .Where(x => x.Category == categoryToDelete).ToListAsync();
            
            dbContext.Products.RemoveRange(productsToDelete);

            int rowsAffected = await dbContext.SaveChangesAsync();

            return Results.Ok($"Rows affected: {rowsAffected}");
        });

        app.MapDelete("/delete-with-bulk",
            async (string categoryToDelete, BulkOperationsDbContext dbContext) =>
        {
            if (string.IsNullOrEmpty(categoryToDelete))
            {
                return Results.BadRequest("Category cannot be null or empty.");
            }

            int rowsAffected = await dbContext.Products
                .Where(x => x.Category == categoryToDelete)
                .ExecuteDeleteAsync();

            return Results.Ok($"Rows affected: {rowsAffected}");
        });
    }
}