using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAppEntities.Migrations
{
    /// <inheritdoc />
    public partial class AllTasks_SP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetTasks = @"
                CREATE PROCEDURE [dbo].[GetTasks]
                As begin
                    Select * From [dbo].[GetTasks];
                End
            ";
            migrationBuilder.Sql(sp_GetTasks);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetTasks = @"
                DROP PROCEDURE [dbo].[GetTasks]
               ";
            migrationBuilder.Sql(sp_GetTasks);
        }
    }
}
