using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class purcha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "purchasereturnpaymenthistries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    billid = table.Column<int>(nullable: false),
                    Customername = table.Column<string>(nullable: true),
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
                    table.PrimaryKey("PK_purchasereturnpaymenthistries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchasereturns",
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
                    Branch = table.Column<string>(nullable: true),
                    Resion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchasereturns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purchasereturnsummeries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Billid = table.Column<int>(nullable: false),
                    Totalqty = table.Column<int>(nullable: false),
                    Totalamount = table.Column<decimal>(nullable: false),
                    Gst = table.Column<int>(nullable: false),
                    Paymenttype = table.Column<string>(nullable: true),
                    Refcode = table.Column<string>(nullable: true),
                    Paid = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Customername = table.Column<string>(nullable: true),
                    Mobilenumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Billdate = table.Column<DateTime>(nullable: false),
                    Billby = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchasereturnsummeries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tmppurchasereturns",
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
                    Branch = table.Column<string>(nullable: true),
                    Reasion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tmppurchasereturns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchasereturnpaymenthistries");

            migrationBuilder.DropTable(
                name: "purchasereturns");

            migrationBuilder.DropTable(
                name: "purchasereturnsummeries");

            migrationBuilder.DropTable(
                name: "tmppurchasereturns");
        }
    }
}
