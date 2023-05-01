using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitializeWorkingDayTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkingDays",
                columns: new[] { "WorkingDayId", "EndTime", "IsActive", "IsDeleted", "LastModifiedDate", "StartTime", "WorkingDayName" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 16, 0, 0, 0), true, false, null, new TimeSpan(0, 10, 0, 0, 0), "Monday" },
                    { 2, new TimeSpan(0, 16, 0, 0, 0), true, false, null, new TimeSpan(0, 10, 0, 0, 0), "Tuesday" },
                    { 3, new TimeSpan(0, 16, 0, 0, 0), true, false, null, new TimeSpan(0, 10, 0, 0, 0), "Wednesday" },
                    { 4, new TimeSpan(0, 16, 0, 0, 0), true, false, null, new TimeSpan(0, 10, 0, 0, 0), "Thursday" },
                    { 5, new TimeSpan(0, 16, 0, 0, 0), true, false, null, new TimeSpan(0, 10, 0, 0, 0), "Friday" },
                    { 6, new TimeSpan(0, 18, 0, 0, 0), true, false, null, new TimeSpan(0, 7, 0, 0, 0), "Saturday" },
                    { 7, new TimeSpan(0, 18, 0, 0, 0), true, false, null, new TimeSpan(0, 7, 0, 0, 0), "Sunday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 7);
        }
    }
}
