using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class nettotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "nettotal",
                table: "salesinvoicesummery",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nettotal",
                table: "salesinvoicesummery");
        }
    }
}
