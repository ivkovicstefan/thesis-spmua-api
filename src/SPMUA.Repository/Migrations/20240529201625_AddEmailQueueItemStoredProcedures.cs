using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailQueueItemStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[USP_EmailQueueItem_Set]
(
	@EmailQueueId INT,
	@NoOfAttempts INT,
	@EmailQueueStatusId INT
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
			UPDATE 
				dbo.[EmailQueue]
			SET
				NoOfAttempts = @NoOfAttempts,
				EmailQueueStatusId = @EmailQueueStatusId
			WHERE
				EmailQueueId = @EmailQueueId
		COMMIT
	END TRY
	BEGIN CATCH
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK
			END
	END CATCH
END
			");

            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[USP_EmailQueueItems_Get] 
(
	@NoOfItems INT,
	@MaxNoOfAttempts INT
)
AS
BEGIN
	SELECT TOP (@NoOfItems)
		EmailQueueId,
		ToEmail,
		EmailSubject,
		EmailBody,
		NoOfAttempts,
		EmailQueueStatusId
	FROM 
		dbo.EmailQueue
	WHERE 
		NoOfAttempts < @MaxNoOfAttempts
		AND EmailQueueStatusId <> 2
	ORDER BY
		CreatedDate
END			
			");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[USP_EmailQueueItem_Set]");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[USP_EmailQueueItems_Get]");
        }
    }
}
