using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenameServicesTableToServiceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceTypePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceTypeDuration = table.Column<int>(type: "int", nullable: false),
                    IsAvailableOnMonday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnTuesday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnWednesday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnThursday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnFriday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnSaturday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnSunday = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql:"GETDATE()"),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "ServiceTypeId", "IsActive", "IsAvailableOnFriday", "IsAvailableOnMonday", "IsAvailableOnSaturday", "IsAvailableOnSunday", "IsAvailableOnThursday", "IsAvailableOnTuesday", "IsAvailableOnWednesday", "IsDeleted", "LastModifiedDate", "ServiceTypeDuration", "ServiceTypeName", "ServiceTypePrice" },
                values: new object[,]
                {
                    { 1, true, true, true, true, true, true, true, true, false, null, 60, "Makeup", 2400.00m },
                    { 2, true, true, true, false, false, true, true, true, false, null, 60, "Brow lift", 1500.00m },
                    { 3, true, true, true, false, false, true, true, true, false, null, 90, "Lash lift", 1700.00m },
                    { 4, true, true, true, false, false, true, true, true, false, null, 90, "Brow & Lash lift", 3000.00m }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnFriday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnMonday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnSaturday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnSunday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnThursday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnTuesday = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailableOnWednesday = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceDuration = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "IsActive", "IsAvailableOnFriday", "IsAvailableOnMonday", "IsAvailableOnSaturday", "IsAvailableOnSunday", "IsAvailableOnThursday", "IsAvailableOnTuesday", "IsAvailableOnWednesday", "IsDeleted", "LastModifiedDate", "ServiceDuration", "ServiceName", "ServicePrice" },
                values: new object[,]
                {
                    { 1, true, true, true, true, true, true, true, true, false, null, 60, "Makeup", 2400.00m },
                    { 2, true, true, true, false, false, true, true, true, false, null, 60, "Brow lift", 1500.00m },
                    { 3, true, true, true, false, false, true, true, true, false, null, 90, "Lash lift", 1700.00m },
                    { 4, true, true, true, false, false, true, true, true, false, null, 90, "Brow & Lash lift", 3000.00m }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
