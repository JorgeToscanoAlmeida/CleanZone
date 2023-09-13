using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanZone.Migrations
{
    /// <inheritdoc />
    public partial class cleanproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClean",
                table: "Division",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClean",
                table: "Division");
        }
    }
}
