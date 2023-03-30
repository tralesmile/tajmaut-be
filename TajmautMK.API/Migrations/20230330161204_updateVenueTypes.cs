using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateVenueTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueTypeId",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VenueTypes",
                columns: table => new
                {
                    Venue_TypesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueTypes", x => x.Venue_TypesId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_VenueTypeId",
                table: "Venues",
                column: "VenueTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_VenueTypes_VenueTypeId",
                table: "Venues",
                column: "VenueTypeId",
                principalTable: "VenueTypes",
                principalColumn: "Venue_TypesId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_VenueTypes_VenueTypeId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "VenueTypes");

            migrationBuilder.DropIndex(
                name: "IX_Venues_VenueTypeId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "VenueTypeId",
                table: "Venues");
        }
    }
}
