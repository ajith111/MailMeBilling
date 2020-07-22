using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailMeBilling.Migrations
{
    public partial class mnkl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "drivername",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "drmobile",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employeename",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "empmobile",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ftype",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fuleamount",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fulestation",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "idpro",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "regno",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resonemp",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "totalkg",
                table: "expens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vtype",
                table: "expens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "drivername",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "drmobile",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "employeename",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "empmobile",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "ftype",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "fuleamount",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "fulestation",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "idpro",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "regno",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "resonemp",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "totalkg",
                table: "expens");

            migrationBuilder.DropColumn(
                name: "vtype",
                table: "expens");
        }
    }
}
