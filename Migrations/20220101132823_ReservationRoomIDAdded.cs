using Microsoft.EntityFrameworkCore.Migrations;

namespace LakDerana_HotelChainManagement.Migrations
{
    public partial class ReservationRoomIDAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationID",
                table: "Tbl_ReservedRoom",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Tbl_ReservedRoom");
        }
    }
}
