using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class branch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aboutshop",
                table: "branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "branch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shopname",
                table: "branch",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aboutshop",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "branch");

            migrationBuilder.DropColumn(
                name: "Shopname",
                table: "branch");
        }
    }
}
