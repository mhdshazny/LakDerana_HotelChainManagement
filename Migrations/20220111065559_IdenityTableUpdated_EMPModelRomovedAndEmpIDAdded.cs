using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class IdenityTableUpdated_EMPModelRomovedAndEmpIDAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_EmpLogin_Tbl_Employee_EmpIDID",
                table: "tbl_EmpLogin");

            migrationBuilder.DropIndex(
                name: "IX_tbl_EmpLogin_EmpIDID",
                table: "tbl_EmpLogin");

            migrationBuilder.DropColumn(
                name: "EmpIDID",
                table: "tbl_EmpLogin");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "tbl_EmpLogin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "tbl_EmpLogin");

            migrationBuilder.AddColumn<string>(
                name: "EmpIDID",
                table: "tbl_EmpLogin",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_EmpLogin_EmpIDID",
                table: "tbl_EmpLogin",
                column: "EmpIDID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_EmpLogin_Tbl_Employee_EmpIDID",
                table: "tbl_EmpLogin",
                column: "EmpIDID",
                principalTable: "Tbl_Employee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
