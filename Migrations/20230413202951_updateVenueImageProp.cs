using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateVenueImageProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VenueImage",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VenueImage",
                table: "Venues");
        }
    }
}
