using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorecaInfrastructure.Migrations
{
    public partial class floorplansss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floorplan_Restaurants_RestaurantId",
                table: "Floorplan");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_Floorplan_FloorplanId",
                table: "Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table",
                table: "Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floorplan",
                table: "Floorplan");

            migrationBuilder.RenameTable(
                name: "Table",
                newName: "Tables");

            migrationBuilder.RenameTable(
                name: "Floorplan",
                newName: "Floorplans");

            migrationBuilder.RenameIndex(
                name: "IX_Table_FloorplanId",
                table: "Tables",
                newName: "IX_Tables_FloorplanId");

            migrationBuilder.RenameIndex(
                name: "IX_Floorplan_RestaurantId",
                table: "Floorplans",
                newName: "IX_Floorplans_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tables",
                table: "Tables",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floorplans",
                table: "Floorplans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Floorplans_Restaurants_RestaurantId",
                table: "Floorplans",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Floorplans_FloorplanId",
                table: "Tables",
                column: "FloorplanId",
                principalTable: "Floorplans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floorplans_Restaurants_RestaurantId",
                table: "Floorplans");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Floorplans_FloorplanId",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tables",
                table: "Tables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Floorplans",
                table: "Floorplans");

            migrationBuilder.RenameTable(
                name: "Tables",
                newName: "Table");

            migrationBuilder.RenameTable(
                name: "Floorplans",
                newName: "Floorplan");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_FloorplanId",
                table: "Table",
                newName: "IX_Table_FloorplanId");

            migrationBuilder.RenameIndex(
                name: "IX_Floorplans_RestaurantId",
                table: "Floorplan",
                newName: "IX_Floorplan_RestaurantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table",
                table: "Table",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Floorplan",
                table: "Floorplan",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Floorplan_Restaurants_RestaurantId",
                table: "Floorplan",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Table_Floorplan_FloorplanId",
                table: "Table",
                column: "FloorplanId",
                principalTable: "Floorplan",
                principalColumn: "Id");
        }
    }
}
