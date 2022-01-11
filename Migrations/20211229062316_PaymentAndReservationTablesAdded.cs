using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class PaymentAndReservationTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRoomCharge",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "SpecialDescription",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "TotalPayableAmount",
                table: "Tbl_Reservation");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Tbl_Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutDate",
                table: "Tbl_Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Tbl_Reservation",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "Tbl_Payments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationID = table.Column<string>(nullable: false),
                    ReservationFee = table.Column<decimal>(type: "money", nullable: false),
                    HotelServiceCharge = table.Column<decimal>(type: "money", nullable: false),
                    NetAmount = table.Column<decimal>(type: "money", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalPayableAmount = table.Column<decimal>(type: "money", nullable: false),
                    AdvancePaymentAmount = table.Column<decimal>(type: "money", nullable: false),
                    FinalPaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Payments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ReservedRoom",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    SpecialDescription = table.Column<string>(nullable: true),
                    CurrentRoomCharge = table.Column<decimal>(type: "money", nullable: false),
                    TotalPayableAmount = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ReservedRoom", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Payments");

            migrationBuilder.DropTable(
                name: "Tbl_ReservedRoom");

            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "CheckoutDate",
                table: "Tbl_Reservation");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tbl_Reservation");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentRoomCharge",
                table: "Tbl_Reservation",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "Tbl_Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReservationStatus",
                table: "Tbl_Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Tbl_Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialDescription",
                table: "Tbl_Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "Tbl_Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayableAmount",
                table: "Tbl_Reservation",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
