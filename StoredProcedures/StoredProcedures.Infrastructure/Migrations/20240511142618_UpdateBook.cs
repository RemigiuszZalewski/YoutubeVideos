using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "CREATE PROCEDURE UpdateBook\n\t@bookId INT,\n    @name NVARCHAR(100),\n\t@category NVARCHAR(100),\n    @authorId INT\nAS\nBEGIN\n\tSET NOCOUNT ON;\n    UPDATE Books SET Name = @name, Category = @category, AuthorId = @authorId\n\tWHERE Books.Id = @bookId\nEND";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[UpdateBook]";

            migrationBuilder.Sql(sql);
        }
    }
}
