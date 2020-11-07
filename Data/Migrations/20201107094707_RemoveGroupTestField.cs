using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Data.Migrations
{
    public partial class RemoveGroupTestField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestField",
                table: "Group");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestField",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
