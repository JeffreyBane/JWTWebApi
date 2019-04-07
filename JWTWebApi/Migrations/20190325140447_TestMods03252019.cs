using Microsoft.EntityFrameworkCore.Migrations;

namespace EFXServices.Migrations
{
    public partial class TestMods03252019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YetAnotherProperty",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YetAnotherProperty",
                table: "Test");
        }
    }
}
