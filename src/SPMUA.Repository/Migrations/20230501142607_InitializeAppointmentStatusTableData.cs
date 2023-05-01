using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitializeAppointmentStatusTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppointmentStatus",
                columns: new[] { "AppointmentStatusId", "AppointmentStatusName", "IsActive", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Confirmation Pending", true, false },
                    { 2, "Confirmed", true, false },
                    { 3, "Rejected", true, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 3);
        }
    }
}
