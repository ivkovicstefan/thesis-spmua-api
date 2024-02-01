using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPMUA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitializeAdminsTableData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "AdminEmail", "AdminFirstName", "AdminLastName", "PasswordHash" },
                values: new object[] { 1, "ivkovics@outlook.com", "System", "Admin", "$2a$11$O49GrWrVzkBO3jvUa9fGY.hh12Clbk6.HumaYdEwi5abLa8R3Q3ca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1);
        }
    }
}
