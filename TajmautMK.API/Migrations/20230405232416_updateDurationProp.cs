﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tajmautAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateDurationProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Events");
        }
    }
}
