﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TajmautMK.API.Migrations
{
    /// <inheritdoc />
    public partial class updateRestaurantPropss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Restaurants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Restaurants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Restaurants");
        }
    }
}
