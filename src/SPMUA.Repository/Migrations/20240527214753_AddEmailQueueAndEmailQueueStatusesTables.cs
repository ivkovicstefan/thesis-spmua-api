using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailQueueAndEmailQueueStatusesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailQueueStatuses",
                columns: table => new
                {
                    EmailQueueStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailQueueStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueueStatuses", x => x.EmailQueueStatusId);
                });

            migrationBuilder.CreateTable(
                name: "EmailQueue",
                columns: table => new
                {
                    EmailQueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailBody = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    EmailQueueStatusId = table.Column<int>(type: "int", nullable: false),
                    NoOfAttempts = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueue", x => x.EmailQueueId);
                    table.ForeignKey(
                        name: "FK_EmailQueue_EmailQueueStatuses_EmailQueueStatusId",
                        column: x => x.EmailQueueStatusId,
                        principalTable: "EmailQueueStatuses",
                        principalColumn: "EmailQueueStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmailQueueStatuses",
                columns: new[] { "EmailQueueStatusId", "EmailQueueStatusName" },
                values: new object[,]
                {
                    { 1, "Ready" },
                    { 2, "Sent" },
                    { 3, "Failed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailQueue_EmailQueueStatusId",
                table: "EmailQueue",
                column: "EmailQueueStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailQueue");

            migrationBuilder.DropTable(
                name: "EmailQueueStatuses");
        }
    }
}
