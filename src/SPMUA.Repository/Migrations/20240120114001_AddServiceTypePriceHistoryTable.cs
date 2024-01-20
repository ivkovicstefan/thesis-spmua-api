using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTypePriceHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTypePrice",
                table: "ServiceTypes");

            migrationBuilder.CreateTable(
                name: "ServiceTypePriceHistory",
                columns: table => new
                {
                    ServiceTypePriceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypePriceHistory", x => x.ServiceTypePriceHistoryId);
                    table.ForeignKey(
                        name: "FK_ServiceTypePriceHistory_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ServiceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ServiceTypePriceHistory",
                columns: new[] { "ServiceTypePriceHistoryId", "ServiceTypeId", "ServiceTypePrice" },
                values: new object[,]
                {
                    { 1, 1, 2400.00m },
                    { 2, 2, 1500.00m },
                    { 3, 3, 1700.00m },
                    { 4, 4, 3000.00m }
                });

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 1,
                column: "WorkingDayName",
                value: "Ponedeljak");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 2,
                column: "WorkingDayName",
                value: "Utorak");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 3,
                column: "WorkingDayName",
                value: "Sreda");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 4,
                column: "WorkingDayName",
                value: "Četvrtak");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 5,
                column: "WorkingDayName",
                value: "Petak");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 6,
                column: "WorkingDayName",
                value: "Subota");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 7,
                column: "WorkingDayName",
                value: "Nedelja");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypePriceHistory_ServiceTypeId",
                table: "ServiceTypePriceHistory",
                column: "ServiceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceTypePriceHistory");

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceTypePrice",
                table: "ServiceTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 1,
                column: "ServiceTypePrice",
                value: 2400.00m);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 2,
                column: "ServiceTypePrice",
                value: 1500.00m);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 3,
                column: "ServiceTypePrice",
                value: 1700.00m);

            migrationBuilder.UpdateData(
                table: "ServiceTypes",
                keyColumn: "ServiceTypeId",
                keyValue: 4,
                column: "ServiceTypePrice",
                value: 3000.00m);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 1,
                column: "WorkingDayName",
                value: "Monday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 2,
                column: "WorkingDayName",
                value: "Tuesday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 3,
                column: "WorkingDayName",
                value: "Wednesday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 4,
                column: "WorkingDayName",
                value: "Thursday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 5,
                column: "WorkingDayName",
                value: "Friday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 6,
                column: "WorkingDayName",
                value: "Saturday");

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 7,
                column: "WorkingDayName",
                value: "Sunday");
        }
    }
}
