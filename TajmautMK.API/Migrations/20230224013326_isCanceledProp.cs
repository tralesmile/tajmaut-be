using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TajmautMK.API.Migrations
{
    /// <inheritdoc />
    public partial class isCanceledProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Events",
                newName: "isCanceled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCanceled",
                table: "Events",
                newName: "isActive");
        }
    }
}
