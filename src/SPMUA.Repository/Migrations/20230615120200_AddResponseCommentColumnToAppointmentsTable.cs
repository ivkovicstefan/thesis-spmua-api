using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseCommentColumnToAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseComment",
                table: "Appointments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseComment",
                table: "Appointments");
        }
    }
}
