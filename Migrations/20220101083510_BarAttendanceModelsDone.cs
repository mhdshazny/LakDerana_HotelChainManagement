using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class BarAttendanceModelsDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_AttendanceMaintenance",
                columns: table => new
                {
                    AttendanceLogID = table.Column<string>(nullable: false),
                    AttendanceDate = table.Column<DateTime>(nullable: false),
                    DayDescription = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_AttendanceMaintenance", x => x.AttendanceLogID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_AttendanceSheet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttendanceLogID = table.Column<string>(nullable: false),
                    EmpID = table.Column<string>(nullable: false),
                    AttendanceStatus = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_AttendanceSheet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BarIncomeExpense",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<string>(nullable: false),
                    EmpUpdatedBy = table.Column<string>(nullable: false),
                    IncomeExpenseUpdatedDate = table.Column<DateTime>(nullable: false),
                    Expense = table.Column<decimal>(type: "money", nullable: false),
                    Sales = table.Column<decimal>(type: "money", nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_BarIncomeExpense", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_AttendanceMaintenance");

            migrationBuilder.DropTable(
                name: "Tbl_AttendanceSheet");

            migrationBuilder.DropTable(
                name: "Tbl_BarIncomeExpense");
        }
    }
}
