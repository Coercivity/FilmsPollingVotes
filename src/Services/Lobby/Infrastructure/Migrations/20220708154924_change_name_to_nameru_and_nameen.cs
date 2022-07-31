using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class change_name_to_nameru_and_nameen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Film");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameRu",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "NameRu",
                table: "Film");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Film",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);
        }
    }
}
