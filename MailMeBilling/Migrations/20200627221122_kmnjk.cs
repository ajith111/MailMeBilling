using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class kmnjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "billid",
                table: "customerpaymenthistry",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "billid",
                table: "customerpaymenthistry");
        }
    }
}
