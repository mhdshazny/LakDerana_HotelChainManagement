using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class InitialMigrationAfterClearingRelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Customer",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    CusfName = table.Column<string>(nullable: false),
                    CuslName = table.Column<string>(nullable: false),
                    CusNIC = table.Column<string>(nullable: false),
                    CusGender = table.Column<string>(nullable: false),
                    CusAddress = table.Column<string>(nullable: false),
                    CusContact = table.Column<string>(nullable: false),
                    CusEmail = table.Column<string>(nullable: false),
                    CusStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Employee",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    fName = table.Column<string>(nullable: true),
                    lName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DoB = table.Column<DateTime>(nullable: false),
                    NIC = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Employee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Hotel",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    HotelSpecialName = table.Column<string>(nullable: true),
                    HotelLocation = table.Column<string>(nullable: true),
                    HotelManager = table.Column<string>(nullable: true),
                    RoomServiceManager = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Hotel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_HotelRoom",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<string>(nullable: true),
                    RoomType = table.Column<string>(nullable: true),
                    RoomPricePerNight = table.Column<decimal>(type: "money", nullable: false),
                    RoomDescription = table.Column<string>(nullable: true),
                    RoomStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_HotelRoom", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Reservation",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Hotel = table.Column<string>(nullable: true),
                    Room = table.Column<string>(nullable: true),
                    Customer = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    SpecialDescription = table.Column<string>(nullable: true),
                    CurrentRoomCharge = table.Column<decimal>(type: "money", nullable: false),
                    TotalPayableAmount = table.Column<decimal>(type: "money", nullable: false),
                    PaymentStatus = table.Column<string>(nullable: true),
                    ReservationStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Reservation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_EmpLogin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpIDID = table.Column<string>(nullable: true),
                    EmpEmail = table.Column<string>(nullable: true),
                    EmpPassWord = table.Column<string>(nullable: true),
                    EmpRole = table.Column<string>(nullable: true),
                    EmpStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_EmpLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_EmpLogin_Tbl_Employee_EmpIDID",
                        column: x => x.EmpIDID,
                        principalTable: "Tbl_Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmpLogin_EmpIDID",
                table: "tbl_EmpLogin",
                column: "EmpIDID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Customer");

            migrationBuilder.DropTable(
                name: "tbl_EmpLogin");

            migrationBuilder.DropTable(
                name: "Tbl_Hotel");

            migrationBuilder.DropTable(
                name: "Tbl_HotelRoom");

            migrationBuilder.DropTable(
                name: "Tbl_Reservation");

            migrationBuilder.DropTable(
                name: "Tbl_Employee");
        }
    }
}
