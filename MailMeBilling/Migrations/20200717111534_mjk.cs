using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class mjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "addby",
                table: "creditnote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "branch",
                table: "creditnote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "addby",
                table: "creditnote");

            migrationBuilder.DropColumn(
                name: "branch",
                table: "creditnote");
        }
    }
}
