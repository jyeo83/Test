using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.DOM.Migrations
{
    public partial class idTrial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
