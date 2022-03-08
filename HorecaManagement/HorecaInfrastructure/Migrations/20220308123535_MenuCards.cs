using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorecaInfrastructure.Migrations
{
    public partial class MenuCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuCardId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuCardId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuCardId",
                table: "Menus",
                column: "MenuCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_MenuCardId",
                table: "Dishes",
                column: "MenuCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_MenuCards_MenuCardId",
                table: "Dishes",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenuCards_MenuCardId",
                table: "Menus",
                column: "MenuCardId",
                principalTable: "MenuCards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_MenuCards_MenuCardId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenuCards_MenuCardId",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "MenuCards");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuCardId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_MenuCardId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "MenuCardId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuCardId",
                table: "Dishes");
        }
    }
}
