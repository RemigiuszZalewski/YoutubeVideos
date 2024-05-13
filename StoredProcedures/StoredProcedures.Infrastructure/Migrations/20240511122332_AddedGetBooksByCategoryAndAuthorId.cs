using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedGetBooksByCategoryAndAuthorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
                "CREATE PROCEDURE GetBooksByCategoryAndAuthorId\n    @category NVARCHAR(100),\n    @authorId INT\nAS\nBEGIN\n\tSET NOCOUNT ON;\n    SELECT * FROM Books\n    WHERE Category = @category AND AuthorId = @authorId;\nEND";
            
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP PROCEDURE GetBooksByCategoryAndAuthorId";

            migrationBuilder.Sql(sql);
        }
    }
}
