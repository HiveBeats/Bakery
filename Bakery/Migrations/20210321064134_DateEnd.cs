using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Bakery.Migrations
{
    public partial class DateEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeId",
                table: "DiscountTime",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "CustomerDiscount",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "CustomerDiscount",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "CustomerDiscount",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "CustomerAddress",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "CustomerAddress",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "CustomerAddress",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Customer",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDiscount_DateEnd",
                table: "CustomerDiscount",
                column: "DateEnd");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_DateEnd",
                table: "CustomerAddress",
                column: "DateEnd");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DateEnd",
                table: "Customer",
                column: "DateEnd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerDiscount_DateEnd",
                table: "CustomerDiscount");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddress_DateEnd",
                table: "CustomerAddress");

            migrationBuilder.DropIndex(
                name: "IX_Customer_DateEnd",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "CustomerDiscount");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "CustomerDiscount");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "CustomerAddress");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "CustomerAddress");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "TimeId",
                table: "DiscountTime",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "CustomerDiscount",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "CustomerAddress",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Customer",
                type: "int(11)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(11)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);
        }
    }
}
