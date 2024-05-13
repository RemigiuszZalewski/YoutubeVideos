using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "CREATE PROCEDURE CreateBook\n    @name NVARCHAR(100),\n\t@category NVARCHAR(100),\n    @authorId INT\nAS\nBEGIN\n\tSET NOCOUNT ON;\n    INSERT INTO Books (Name, Category, AuthorId) VALUES (@name, @category, @authorId);\nEND";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE [dbo].[CreateBook]";
            migrationBuilder.Sql(sql);
        }
    }
}
