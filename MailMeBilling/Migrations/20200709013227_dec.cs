using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class dec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Sgst",
                table: "salesinvoicesummery",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Igst",
                table: "salesinvoicesummery",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cgst",
                table: "salesinvoicesummery",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sgst",
                table: "salesinvoicesummery",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Igst",
                table: "salesinvoicesummery",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Cgst",
                table: "salesinvoicesummery",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
