using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codebridge.Migrations
{
    public partial class ChangeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Dogs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Dogs");
        }
    }
}
