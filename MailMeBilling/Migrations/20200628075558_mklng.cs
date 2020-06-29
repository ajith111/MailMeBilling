using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class mklng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "vendor",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "vendor");
        }
    }
}
