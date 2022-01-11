using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class IdenityTableUpdated_AddedHotelIDToEmpIDentityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelID",
                table: "tbl_EmpLogin",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelID",
                table: "tbl_EmpLogin");
        }
    }
}
