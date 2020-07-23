using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class upex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fuleamount",
                table: "expens");

            migrationBuilder.AddColumn<string>(
                name: "dic",
                table: "expens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dic",
                table: "expens");

            migrationBuilder.AddColumn<string>(
                name: "fuleamount",
                table: "expens",
                type: "text",
                nullable: true);
        }
    }
}
