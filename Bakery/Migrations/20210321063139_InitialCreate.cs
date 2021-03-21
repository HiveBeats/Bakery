using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakery.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int(11)", nullable: false),
                    CustomerName = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int(11)", nullable: false),
                    AddressId = table.Column<int>(type: "int(11)", nullable: false),
                    AddressName = table.Column<string>(maxLength: 255, nullable: true),
                    Latitude = table.Column<float>(type: "float(10,6)", nullable: false),
                    Longitude = table.Column<float>(type: "float(10,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.CustomerId, x.AddressId });
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
                    CustomerId = table.Column<int>(type: "int(11)", nullable: false),
                    DiscountId = table.Column<int>(type: "int(11)", nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.DiscountId, x.CustomerId });
                    table.UniqueConstraint("AK_CustomerDiscount_DiscountId", x => x.DiscountId);
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
                    DiscountId = table.Column<int>(type: "int(11)", nullable: false),
                    TimeId = table.Column<int>(type: "int(11)", nullable: false),
                    DayWeek = table.Column<int>(type: "int(11)", nullable: false),
                    StartTime = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EndTime = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.TimeId, x.DiscountId });
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
                name: "AddressId_UNIQUE",
                table: "CustomerAddress",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_DISCOUNT_CUSTOMER_idx",
                table: "CustomerDiscount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "DiscountId_UNIQUE",
                table: "CustomerDiscount",
                column: "DiscountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_DISCOUNT_TIMES_DISCOUNT",
                table: "DiscountTime",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "TimeId_UNIQUE",
                table: "DiscountTime",
                column: "TimeId",
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
