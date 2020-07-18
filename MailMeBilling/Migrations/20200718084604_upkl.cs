using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class upkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "creditnote");

            migrationBuilder.DropColumn(
                name: "qty",
                table: "creditnote");

            migrationBuilder.AddColumn<string>(
                name: "paymenttype",
                table: "creditnote",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refno",
                table: "creditnote",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "paymenttype",
                table: "creditnote");

            migrationBuilder.DropColumn(
                name: "refno",
                table: "creditnote");

            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "creditnote",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "qty",
                table: "creditnote",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
