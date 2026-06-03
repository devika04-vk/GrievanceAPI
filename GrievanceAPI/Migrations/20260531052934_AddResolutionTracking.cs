using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrievanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddResolutionTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Remarks",
                table: "Complaints",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Complaints",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Complaints");
        }
    }
}
