using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToPluralConvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AppointmentStatus_AppointmentStatusId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Service_ServiceId",
                table: "Appointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacation",
                table: "Vacation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentStatus",
                table: "AppointmentStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Vacation",
                newName: "Vacations");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "EmailTemplate",
                newName: "EmailTemplates");

            migrationBuilder.RenameTable(
                name: "AppointmentStatus",
                newName: "AppointmentStatuses");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ServiceId",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_AppointmentStatusId",
                table: "Appointments",
                newName: "IX_Appointments_AppointmentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacations",
                table: "Vacations",
                column: "VacationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates",
                column: "EmailTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentStatuses",
                table: "AppointmentStatuses",
                column: "AppointmentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.UpdateData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "AppointmentStatuses",
                keyColumn: "AppointmentStatusId",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: 4,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 2,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 4,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 5,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 6,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 7,
                column: "IsActive",
                value: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments",
                column: "AppointmentStatusId",
                principalTable: "AppointmentStatuses",
                principalColumn: "AppointmentStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AppointmentStatuses_AppointmentStatusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacations",
                table: "Vacations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentStatuses",
                table: "AppointmentStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Vacations",
                newName: "Vacation");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                newName: "EmailTemplate");

            migrationBuilder.RenameTable(
                name: "AppointmentStatuses",
                newName: "AppointmentStatus");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointment",
                newName: "IX_Appointment_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AppointmentStatusId",
                table: "Appointment",
                newName: "IX_Appointment_AppointmentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacation",
                table: "Vacation",
                column: "VacationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplate",
                table: "EmailTemplate",
                column: "EmailTemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentStatus",
                table: "AppointmentStatus",
                column: "AppointmentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.UpdateData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "AppointmentStatus",
                keyColumn: "AppointmentStatusId",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Service",
                keyColumn: "ServiceId",
                keyValue: 4,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 2,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 4,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 5,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 6,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "WorkingDays",
                keyColumn: "WorkingDayId",
                keyValue: 7,
                column: "IsActive",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AppointmentStatus_AppointmentStatusId",
                table: "Appointment",
                column: "AppointmentStatusId",
                principalTable: "AppointmentStatus",
                principalColumn: "AppointmentStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Service_ServiceId",
                table: "Appointment",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
