using Microsoft.EntityFrameworkCore.Migrations;

namespace Velvetech.Data.Migrations
{
    public partial class GroupTestField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestField",
                table: "Group",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestField",
                table: "Group");
        }
    }
}
