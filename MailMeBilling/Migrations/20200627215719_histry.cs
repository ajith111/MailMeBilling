using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class histry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerpaymenthistry",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerpaymenthistry");
        }
    }
}
