using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakery.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerDescription = table.Column<string>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    AddressId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    AddressName = table.Column<string>(maxLength: 255, nullable: true),
                    Latitude = table.Column<float>(type: "float(10,6)", nullable: false),
                    Longitude = table.Column<float>(type: "float(10,6)", nullable: false),
                    DateStart = table.Column<DateTime>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ADDRESS_CUSTOMER",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDiscount",
                columns: table => new
                {
                    DiscountId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_DISCOUNT_CUSTOMER",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTime",
                columns: table => new
                {
                    TimeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiscountId = table.Column<long>(nullable: false),
                    DayWeek = table.Column<int>(type: "int(11)", nullable: false),
                    StartTime = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EndTime = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TimeId);
                    table.ForeignKey(
                        name: "FK_DISCOUNT_TIMES_DISCOUNT",
                        column: x => x.DiscountId,
                        principalTable: "CustomerDiscount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_CUSTOMER_NAME",
                table: "Customer",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DateEnd",
                table: "Customer",
                column: "DateEnd");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_DateEnd",
                table: "CustomerAddress",
                column: "DateEnd");

            migrationBuilder.CreateIndex(
                name: "Customer_AddressId_UNIQUE",
                table: "CustomerAddress",
                columns: new[] { "CustomerId", "AddressId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDiscount_CustomerId",
                table: "CustomerDiscount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDiscount_DateEnd",
                table: "CustomerDiscount",
                column: "DateEnd");

            migrationBuilder.CreateIndex(
                name: "CustomerId_DiscountId_UNIQUE",
                table: "CustomerDiscount",
                columns: new[] { "DiscountId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_DISCOUNT_TIMES_DISCOUNT",
                table: "DiscountTime",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "DiscountId_TimeId_UNIQUE",
                table: "DiscountTime",
                columns: new[] { "TimeId", "DiscountId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "DiscountTime");

            migrationBuilder.DropTable(
                name: "CustomerDiscount");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
