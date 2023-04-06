using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatePropsVenueCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Venue_CityId",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Venue_Cities",
                columns: table => new
                {
                    Venue_CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venue_Cities", x => x.Venue_CityId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_Venue_CityId",
                table: "Venues",
                column: "Venue_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Venue_Cities_Venue_CityId",
                table: "Venues",
                column: "Venue_CityId",
                principalTable: "Venue_Cities",
                principalColumn: "Venue_CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Venue_Cities_Venue_CityId",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "Venue_Cities");

            migrationBuilder.DropIndex(
                name: "IX_Venues_Venue_CityId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "Venue_CityId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Events");
        }
    }
}
