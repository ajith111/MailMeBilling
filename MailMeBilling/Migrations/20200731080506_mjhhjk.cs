using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class mjhhjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creditcustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Customername = table.Column<string>(nullable: false),
                    Mobilenumber = table.Column<string>(nullable: false),
                    Emailid = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    Points = table.Column<long>(nullable: false),
                    Shopid = table.Column<int>(nullable: false),
                    Entrydate = table.Column<DateTime>(nullable: false),
                    Entryby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditcustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "creditvendors",
                columns: table => new
                {
                    vendorId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Mobilenumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Bankname = table.Column<string>(nullable: true),
                    Accountnumber = table.Column<string>(nullable: true),
                    Ifsccode = table.Column<string>(nullable: true),
                    bankbranch = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    Entrydate = table.Column<DateTime>(nullable: false),
                    Entryby = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditvendors", x => x.vendorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creditcustomers");

            migrationBuilder.DropTable(
                name: "creditvendors");
        }
    }
}
