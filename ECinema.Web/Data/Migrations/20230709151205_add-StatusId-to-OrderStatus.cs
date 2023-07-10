using Microsoft.EntityFrameworkCore.Migrations;

namespace ECinema.Web.Data.Migrations
{
    public partial class addStatusIdtoOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "OrderStatuses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "OrderStatuses");
        }
    }
}
