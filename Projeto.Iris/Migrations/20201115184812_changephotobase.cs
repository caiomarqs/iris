using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto.Iris.Migrations
{
    public partial class changephotobase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBase64",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PhotoBase64Location",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBase64Location",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "PhotoBase64",
                table: "Photos",
                type: "VARCHAR(MAX)",
                nullable: true);
        }
    }
}
