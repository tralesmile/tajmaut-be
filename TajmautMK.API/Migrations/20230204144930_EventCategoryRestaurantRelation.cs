using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class EventCategoryRestaurantRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryEventId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryEvents",
                columns: table => new
                {
                    CategoryEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEvents", x => x.CategoryEventId);
                    table.ForeignKey(
                        name: "FK_CategoryEvents_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryEventId",
                table: "Events",
                column: "CategoryEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEvents_RestaurantId",
                table: "CategoryEvents",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_CategoryEvents_CategoryEventId",
                table: "Events",
                column: "CategoryEventId",
                principalTable: "CategoryEvents",
                principalColumn: "CategoryEventId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_CategoryEvents_CategoryEventId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "CategoryEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_CategoryEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CategoryEventId",
                table: "Events");
        }
    }
}
