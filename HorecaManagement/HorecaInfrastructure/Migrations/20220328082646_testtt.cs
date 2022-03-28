using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorecaInfrastructure.Migrations
{
    public partial class testtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Restaurants_RestaurantId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RestaurantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserRestaurant",
                columns: table => new
                {
                    EmployeesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRestaurant", x => new { x.EmployeesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRestaurant_AspNetUsers_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRestaurant_Restaurants_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRestaurant_RestaurantsId",
                table: "ApplicationUserRestaurant",
                column: "RestaurantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRestaurant");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RestaurantId",
                table: "AspNetUsers",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Restaurants_RestaurantId",
                table: "AspNetUsers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
