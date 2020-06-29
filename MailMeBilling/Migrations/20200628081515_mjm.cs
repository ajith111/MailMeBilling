using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class mjm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mobilenumber",
                table: "vendor",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Entryby",
                table: "vendor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Entrydate",
                table: "vendor",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "vendorpayments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    billid = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    paymenttype = table.Column<string>(nullable: true),
                    refno = table.Column<string>(nullable: true),
                    Payment = table.Column<decimal>(nullable: false),
                    total = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Paiddate = table.Column<DateTime>(nullable: false),
                    Recivedby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendorpayments", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vendorpayments");

            migrationBuilder.DropColumn(
                name: "Entryby",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "Entrydate",
                table: "vendor");

            migrationBuilder.AlterColumn<long>(
                name: "Mobilenumber",
                table: "vendor",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
