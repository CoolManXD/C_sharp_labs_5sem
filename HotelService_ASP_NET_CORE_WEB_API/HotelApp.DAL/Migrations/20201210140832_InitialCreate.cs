using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelApp.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "TypeComfort",
                columns: table => new
                {
                    TypeComfortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comfort = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeComfort", x => x.TypeComfortId);
                });

            migrationBuilder.CreateTable(
                name: "TypeSize",
                columns: table => new
                {
                    TypeSizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeSize", x => x.TypeSizeId);
                });

            migrationBuilder.CreateTable(
                name: "HotelRooms",
                columns: table => new
                {
                    HotelRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeSizeId = table.Column<int>(type: "int", nullable: false),
                    TypeComfortId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRooms", x => x.HotelRoomId);
                    table.ForeignKey(
                        name: "FK_HotelRooms_TypeComfort_TypeComfortId",
                        column: x => x.TypeComfortId,
                        principalTable: "TypeComfort",
                        principalColumn: "TypeComfortId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRooms_TypeSize_TypeSizeId",
                        column: x => x.TypeSizeId,
                        principalTable: "TypeSize",
                        principalColumn: "TypeSizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveOrders",
                columns: table => new
                {
                    ActiveOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    HotelRoomId = table.Column<int>(type: "int", nullable: false),
                    PaymentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveOrders", x => x.ActiveOrderId);
                    table.ForeignKey(
                        name: "FK_ActiveOrders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveOrders_HotelRooms_HotelRoomId",
                        column: x => x.HotelRoomId,
                        principalTable: "HotelRooms",
                        principalColumn: "HotelRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TypeComfort",
                columns: new[] { "TypeComfortId", "Comfort" },
                values: new object[,]
                {
                    { 1, "Standart" },
                    { 2, "Suite" },
                    { 3, "De_Luxe" },
                    { 4, "Duplex" },
                    { 5, "Family_Room" },
                    { 6, "Honeymoon_Room" }
                });

            migrationBuilder.InsertData(
                table: "TypeSize",
                columns: new[] { "TypeSizeId", "Size" },
                values: new object[,]
                {
                    { 1, "SGL" },
                    { 2, "DBL" },
                    { 3, "DBL_TWN" },
                    { 4, "TRPL" },
                    { 5, "DBL_EXB" },
                    { 6, "TRPL_EXB" }
                });

            migrationBuilder.InsertData(
                table: "HotelRooms",
                columns: new[] { "HotelRoomId", "Number", "PricePerDay", "TypeComfortId", "TypeSizeId" },
                values: new object[,]
                {
                    { 1, "10", 100m, 1, 1 },
                    { 2, "11", 100m, 1, 1 },
                    { 3, "12", 200m, 2, 1 },
                    { 7, "22", 300m, 3, 1 },
                    { 4, "13", 200m, 2, 2 },
                    { 8, "30", 400m, 3, 2 },
                    { 11, "50", 800m, 6, 2 },
                    { 5, "20", 250m, 2, 3 },
                    { 9, "31", 400m, 4, 3 },
                    { 6, "21", 250m, 2, 5 },
                    { 10, "40", 600m, 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveOrders_ClientId",
                table: "ActiveOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveOrders_HotelRoomId",
                table: "ActiveOrders",
                column: "HotelRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_TypeComfortId",
                table: "HotelRooms",
                column: "TypeComfortId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_TypeSizeId",
                table: "HotelRooms",
                column: "TypeSizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveOrders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "HotelRooms");

            migrationBuilder.DropTable(
                name: "TypeComfort");

            migrationBuilder.DropTable(
                name: "TypeSize");
        }
    }
}
