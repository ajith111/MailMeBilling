using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class kl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Catagory",
                table: "category");

            migrationBuilder.DropColumn(
                name: "Catagorydiscription",
                table: "category");

            migrationBuilder.AddColumn<string>(
                name: "Categorydiscription",
                table: "category",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Categorys",
                table: "category",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categorydiscription",
                table: "category");

            migrationBuilder.DropColumn(
                name: "Categorys",
                table: "category");

            migrationBuilder.AddColumn<string>(
                name: "Catagory",
                table: "category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Catagorydiscription",
                table: "category",
                type: "text",
                nullable: true);
        }
    }
}
