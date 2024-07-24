using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAppEntities.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSPName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetTask = @"
                ALTER PROCEDURE [dbo].[GetTasks]
                AS 
                BEGIN 
                    Select * From MyTaskDetails;
                END
            ";
            migrationBuilder.Sql(sp_GetTask);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
