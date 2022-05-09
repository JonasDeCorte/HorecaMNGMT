using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorecaInfrastructure.Migrations
{
    public partial class FloorplanName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Menus_MenuId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_BookingDetails_BookingDetailId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_RestaurantSchedules_RestaurantScheduleId",
                table: "Tables");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "RestaurantSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Tables_RestaurantScheduleId",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "RestaurantScheduleId",
                table: "Tables",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "BookingDetailId",
                table: "Tables",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_BookingDetailId",
                table: "Tables",
                newName: "IX_Tables_ScheduleId");

            migrationBuilder.RenameColumn(
                name: "MenuId",
                table: "Dishes",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_MenuId",
                table: "Dishes",
                newName: "IX_Dishes_RestaurantId");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Pax",
                table: "Tables",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Tables",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FloorplanId",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Left",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginX",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OriginY",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ScaleX",
                table: "Tables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ScaleY",
                table: "Tables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Seats",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Top",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Menus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Ingredients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Dishes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Pax",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Floorplans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floorplans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floorplans_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuDishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    menuId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuDishes_Menus_menuId",
                        column: x => x.menuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AvailableSeat = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DishState = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_RestaurantId",
                table: "Units",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_BookingId",
                table: "Tables",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_FloorplanId",
                table: "Tables",
                column: "FloorplanId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menus",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RestaurantId",
                table: "Ingredients",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Bookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Floorplans_RestaurantId",
                table: "Floorplans",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDishes_DishId",
                table: "MenuDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuDishes_menuId",
                table: "MenuDishes",
                column: "menuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_DishId",
                table: "OrderLines",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RestaurantId",
                table: "Schedules",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Schedules_ScheduleId",
                table: "Bookings",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Restaurants_RestaurantId",
                table: "Ingredients",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Bookings_BookingId",
                table: "Tables",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Floorplans_FloorplanId",
                table: "Tables",
                column: "FloorplanId",
                principalTable: "Floorplans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Schedules_ScheduleId",
                table: "Tables",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Restaurants_RestaurantId",
                table: "Units",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Schedules_ScheduleId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Restaurants_RestaurantId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Bookings_BookingId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Floorplans_FloorplanId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Schedules_ScheduleId",
                table: "Tables");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Restaurants_RestaurantId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "Floorplans");

            migrationBuilder.DropTable(
                name: "MenuDishes");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Units_RestaurantId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Tables_BookingId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_FloorplanId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RestaurantId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "FloorplanId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Left",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "OriginX",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "OriginY",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ScaleX",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ScaleY",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Src",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Top",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Pax",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Tables",
                newName: "RestaurantScheduleId");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Tables",
                newName: "BookingDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Tables_ScheduleId",
                table: "Tables",
                newName: "IX_Tables_BookingDetailId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Dishes",
                newName: "MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                newName: "IX_Dishes_MenuId");

            migrationBuilder.AlterColumn<int>(
                name: "Pax",
                table: "Tables",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "Ingredients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RestaurantSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    AvailableSeat = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantSchedules_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    RestaurantScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Pax = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_RestaurantSchedules_RestaurantScheduleId",
                        column: x => x.RestaurantScheduleId,
                        principalTable: "RestaurantSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantScheduleId",
                table: "Tables",
                column: "RestaurantScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_RestaurantScheduleId",
                table: "BookingDetails",
                column: "RestaurantScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantSchedules_RestaurantId",
                table: "RestaurantSchedules",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Menus_MenuId",
                table: "Dishes",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Units_UnitId",
                table: "Ingredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_BookingDetails_BookingDetailId",
                table: "Tables",
                column: "BookingDetailId",
                principalTable: "BookingDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_RestaurantSchedules_RestaurantScheduleId",
                table: "Tables",
                column: "RestaurantScheduleId",
                principalTable: "RestaurantSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
