using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentStatusesTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppointmentStatuses",
                columns: new[] { "AppointmentStatusId", "AppointmentStatusName", "IsActive", "IsDeleted" },
                values: new object[,]
                {
                    { 4, "Completed", true, false },
                    { 5, "Expired", true, false },
                    { 6, "Missed", true, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 6);
        }
    }
}
