using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TajmautMK.API.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEvents_Restaurants_RestaurantId",
                table: "CategoryEvents");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEvents_RestaurantId",
                table: "CategoryEvents");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "CategoryEvents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "CategoryEvents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEvents_RestaurantId",
                table: "CategoryEvents",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEvents_Restaurants_RestaurantId",
                table: "CategoryEvents",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");
        }
    }
}
