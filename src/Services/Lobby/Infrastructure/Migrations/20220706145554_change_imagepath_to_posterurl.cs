using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class change_imagepath_to_posterurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LobbyId",
                table: "Film",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RatingKinopoisk",
                table: "Film",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LobbyId",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "RatingKinopoisk",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Film");
        }
    }
}
