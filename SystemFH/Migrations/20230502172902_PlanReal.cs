using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemFH.Migrations
{
    public partial class PlanReal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanReal",
                table: "CashManager",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanReal",
                table: "CashManager");
        }
    }
}
