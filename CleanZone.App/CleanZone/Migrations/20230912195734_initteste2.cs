using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanZone.Migrations
{
    /// <inheritdoc />
    public partial class initteste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "currentDateFormatted",
                table: "Division",
                newName: "CurrentDateFormatted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentDateFormatted",
                table: "Division",
                newName: "currentDateFormatted");
        }
    }
}
