using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppointmentStatusAutoUpdateStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE USP_AppointmentStatus_AutoUpdate
			AS
			BEGIN
				BEGIN TRY
					BEGIN TRAN
						DECLARE @AppointmentStatusId_Approved INT
							= (SELECT AppointmentStatusId FROM dbo.AppointmentStatuses WHERE AppointmentStatusName = 'Confirmed')

						DECLARE @AppointmentStatusId_Completed INT
							= (SELECT AppointmentStatusId FROM dbo.AppointmentStatuses WHERE AppointmentStatusName = 'Completed')

						DECLARE @AppointmentStatusId_ConfirmationPending INT
							= (SELECT AppointmentStatusId FROM dbo.AppointmentStatuses WHERE AppointmentStatusName = 'Confirmation Pending')

						DECLARE @AppointmentStatusId_Expired INT
							= (SELECT AppointmentStatusId FROM dbo.AppointmentStatuses WHERE AppointmentStatusName = 'Expired')

						UPDATE 
							dbo.Appointments
						SET
							AppointmentStatusId = @AppointmentStatusId_Completed,
							LastModifiedDate = GETDATE()
						WHERE
							AppointmentDate < GETDATE()
							AND AppointmentStatusId = @AppointmentStatusId_Approved

						UPDATE 
							dbo.Appointments
						SET
							AppointmentStatusId = @AppointmentStatusId_Expired,
							LastModifiedDate = GETDATE()
						WHERE
							AppointmentDate < GETDATE()
							AND AppointmentStatusId = @AppointmentStatusId_ConfirmationPending
					COMMIT
				END TRY
				BEGIN CATCH
					ROLLBACK
				END CATCH
			END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS USP_AppointmentStatus_AutoUpdate");
        }
    }
}
