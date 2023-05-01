using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitializeServiceTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "ServiceId", "IsActive", "IsAvailableOnFriday", "IsAvailableOnMonday", "IsAvailableOnSaturday", "IsAvailableOnSunday", "IsAvailableOnThursday", "IsAvailableOnTuesday", "IsAvailableOnWednesday", "IsDeleted", "LastModifiedDate", "ServiceDuration", "ServiceName", "ServicePrice" },
                values: new object[,]
                {
                    { 1, true, true, true, true, true, true, true, true, false, null, 60, "Makeup", 2400.00m },
                    { 2, true, true, true, false, false, true, true, true, false, null, 60, "Brow lift", 1500.00m },
                    { 3, true, true, true, false, false, true, true, true, false, null, 90, "Lash lift", 1700.00m },
                    { 4, true, true, true, false, false, true, true, true, false, null, 90, "Brow & Lash lift", 3000.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 4);
        }
    }
}
