using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace MailMeBilling.Migrations
{
    public partial class ccnote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "creditnote",
                columns: table => new
                {
                    cid = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cdate = table.Column<DateTime>(nullable: false),
                    person = table.Column<string>(nullable: true),
                    particular = table.Column<string>(nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    qty = table.Column<int>(nullable: false),
                    totalamount = table.Column<decimal>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    mobilenumber = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_creditnote", x => x.cid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "creditnote");
        }
    }
}
