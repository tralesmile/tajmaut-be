using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateReservationsProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "OnlineReservations",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OnlineReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "OnlineReservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OnlineReservations");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "OnlineReservations");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "OnlineReservations",
                newName: "FullName");
        }
    }
}
