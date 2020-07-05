using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "branch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Branchname = table.Column<string>(nullable: true),
                    BranchAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Brandname = table.Column<string>(nullable: true),
                    Branddescription = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    categoryid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Categorys = table.Column<string>(nullable: false),
                    Categorydiscription = table.Column<string>(nullable: false),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "color",
                columns: table => new
                {
                    Colorid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Colors = table.Column<string>(nullable: true),
                    Colorsdscription = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_color", x => x.Colorid);
                });

            migrationBuilder.CreateTable(
                name: "customerdetails",
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
                    table.PrimaryKey("PK_customerdetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customerpaymenthistry",
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
                    table.PrimaryKey("PK_customerpaymenthistry", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employeedetails",
                columns: table => new
                {
                    Employeeid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Mobilenumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Roll = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false),
                    Comfirmpassword = table.Column<string>(maxLength: 10, nullable: false),
                    Entrydate = table.Column<DateTime>(nullable: false),
                    Entryby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeedetails", x => x.Employeeid);
                });

            migrationBuilder.CreateTable(
                name: "login",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Userid = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Barcode = table.Column<string>(nullable: true),
                    productname = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    productimage = table.Column<byte[]>(nullable: true),
                    SubcCategory = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Hsncode = table.Column<string>(nullable: true),
                    Purchaserate = table.Column<decimal>(nullable: false),
                    Salesrate = table.Column<decimal>(nullable: false),
                    stock = table.Column<long>(nullable: false),
                    Entrydate = table.Column<DateTime>(nullable: false),
                    Entryby = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productid);
                });

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
                    paid = table.Column<decimal>(nullable: false),
                    upload = table.Column<byte[]>(nullable: true),
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
                name: "salesinvoices",
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
                    table.PrimaryKey("PK_salesinvoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "salesinvoicesummery",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Billid = table.Column<int>(nullable: false),
                    Totalqty = table.Column<int>(nullable: false),
                    Totalamount = table.Column<decimal>(nullable: false),
                    Gst = table.Column<int>(nullable: false),
                    Cgst = table.Column<int>(nullable: false),
                    Sgst = table.Column<int>(nullable: false),
                    Igst = table.Column<int>(nullable: false),
                    Paymenttype = table.Column<string>(nullable: true),
                    Refcode = table.Column<string>(nullable: true),
                    Paid = table.Column<decimal>(nullable: false),
                    discount = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    nettotal = table.Column<decimal>(nullable: false),
                    Billdate = table.Column<DateTime>(nullable: false),
                    Billby = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    Customername = table.Column<string>(nullable: true),
                    Mobilenumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ntow = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salesinvoicesummery", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subcategory",
                columns: table => new
                {
                    Subcategoryid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Subcatagory = table.Column<string>(nullable: true),
                    Subcatagorydiscription = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategory", x => x.Subcategoryid);
                });

            migrationBuilder.CreateTable(
                name: "tempseccions",
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
                    table.PrimaryKey("PK_tempseccions", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "vendor",
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
                    table.PrimaryKey("PK_vendor", x => x.vendorId);
                });

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
                name: "branch");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "color");

            migrationBuilder.DropTable(
                name: "customerdetails");

            migrationBuilder.DropTable(
                name: "customerpaymenthistry");

            migrationBuilder.DropTable(
                name: "employeedetails");

            migrationBuilder.DropTable(
                name: "login");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "purchaseinvoices");

            migrationBuilder.DropTable(
                name: "purchaseinvoicesummeries");

            migrationBuilder.DropTable(
                name: "salesinvoices");

            migrationBuilder.DropTable(
                name: "salesinvoicesummery");

            migrationBuilder.DropTable(
                name: "subcategory");

            migrationBuilder.DropTable(
                name: "tempseccions");

            migrationBuilder.DropTable(
                name: "tmppurchases");

            migrationBuilder.DropTable(
                name: "vendor");

            migrationBuilder.DropTable(
                name: "vendorpayments");
        }
    }
}
