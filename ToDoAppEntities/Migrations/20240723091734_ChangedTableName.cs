using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAppEntities.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailofEmployees",
                table: "DetailofEmployees");

            migrationBuilder.RenameTable(
                name: "DetailofEmployees",
                newName: "MyTaskDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyTaskDetails",
                table: "MyTaskDetails",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyTaskDetails",
                table: "MyTaskDetails");

            migrationBuilder.RenameTable(
                name: "MyTaskDetails",
                newName: "DetailofEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailofEmployees",
                table: "DetailofEmployees",
                column: "TaskId");
        }
    }
}
