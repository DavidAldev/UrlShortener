﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener_Backend.Migrations
{
    /// <inheritdoc />
    public partial class removed_number_visits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVisits",
                table: "Urls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfVisits",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
