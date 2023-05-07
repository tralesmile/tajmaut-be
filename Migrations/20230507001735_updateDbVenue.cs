using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TajmautMK.API.Migrations
{
    /// <inheritdoc />
    public partial class updateDbVenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GalleryImage1",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage2",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage3",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage4",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage5",
                table: "Venues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "lat",
                table: "Venues",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "lng",
                table: "Venues",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryImage1",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "GalleryImage2",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "GalleryImage3",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "GalleryImage4",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "GalleryImage5",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "lat",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "Venues");
        }
    }
}
