using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenameAppointmentTableColumnsRelatedToServiceTypesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Appointments",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceTypeId",
                table: "Appointments",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceTypeId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "Appointments",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceTypeId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ServiceTypes_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
