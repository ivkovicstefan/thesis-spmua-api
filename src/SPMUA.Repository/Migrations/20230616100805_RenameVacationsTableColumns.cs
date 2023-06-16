using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenameVacationsTableColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VacationStartDate",
                table: "Vacations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "VacationEndDate",
                table: "Vacations",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Vacations",
                newName: "VacationStartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Vacations",
                newName: "VacationEndDate");
        }
    }
}
