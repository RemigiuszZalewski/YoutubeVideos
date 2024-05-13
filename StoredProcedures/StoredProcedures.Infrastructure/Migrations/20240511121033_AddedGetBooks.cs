using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedGetBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcedure = "CREATE PROCEDURE [dbo].[GetBooks]\nAS\nBEGIN\n\tSET NOCOUNT ON;\n    SELECT * FROM Books;\nEND";
            migrationBuilder.Sql(createProcedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropStatement = "DROP PROCEDURE [dbo].[GetBooks]";
            migrationBuilder.Sql(dropStatement);
        }
    }
}
