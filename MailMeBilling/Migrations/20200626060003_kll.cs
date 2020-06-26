using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class kll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchaseinvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Productname = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Subcategory = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Hsncode = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Billno = table.Column<int>(nullable: false),
                    Billdate = table.Column<DateTime>(nullable: false),
                    Billby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchaseinvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purchaseinvoicesummeries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Billid = table.Column<int>(nullable: false),
                    Totalqty = table.Column<int>(nullable: false),
                    Totalamount = table.Column<decimal>(nullable: false),
                    Gst = table.Column<string>(nullable: true),
                    Paymenttype = table.Column<string>(nullable: true),
                    Refcode = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    Billdate = table.Column<DateTime>(nullable: false),
                    Billby = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    Vendorrname = table.Column<string>(nullable: true),
                    Mobilenumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ntow = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchaseinvoicesummeries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tmppurchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Productname = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Subcategory = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Hsncode = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Billno = table.Column<int>(nullable: false),
                    Billdate = table.Column<DateTime>(nullable: false),
                    Billby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmppurchases", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchaseinvoices");

            migrationBuilder.DropTable(
                name: "purchaseinvoicesummeries");

            migrationBuilder.DropTable(
                name: "tmppurchases");
        }
    }
}
