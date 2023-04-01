using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Restaurants_RestaurantId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Restaurants_RestaurantId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineReservations_Restaurants_RestaurantId",
                table: "OnlineReservations");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "OnlineReservations",
                newName: "VenueId");

            migrationBuilder.RenameIndex(
                name: "IX_OnlineReservations_RestaurantId",
                table: "OnlineReservations",
                newName: "IX_OnlineReservations_VenueId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Events",
                newName: "VenueId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_RestaurantId",
                table: "Events",
                newName: "IX_Events_VenueId");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Comments",
                newName: "VenueId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_RestaurantId",
                table: "Comments",
                newName: "IX_Comments_VenueId");

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Venues_VenueId",
                table: "Comments",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineReservations_Venues_VenueId",
                table: "OnlineReservations",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "VenueId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Venues_VenueId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Venues_VenueId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineReservations_Venues_VenueId",
                table: "OnlineReservations");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "OnlineReservations",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_OnlineReservations_VenueId",
                table: "OnlineReservations",
                newName: "IX_OnlineReservations_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "Events",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_VenueId",
                table: "Events",
                newName: "IX_Events_RestaurantId");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "Comments",
                newName: "RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VenueId",
                table: "Comments",
                newName: "IX_Comments_RestaurantId");

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Restaurants_RestaurantId",
                table: "Comments",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Restaurants_RestaurantId",
                table: "Events",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineReservations_Restaurants_RestaurantId",
                table: "OnlineReservations",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
