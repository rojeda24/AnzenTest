using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anzen.Migrations
{
    /// <inheritdoc />
    public partial class SubmissionMoreFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EffectiveDate",
                table: "Submission",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpirationDate",
                table: "Submission",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<decimal>(
                name: "Premium",
                table: "Submission",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Sic",
                table: "Submission",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UwName",
                table: "Submission",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "Premium",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "Sic",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "UwName",
                table: "Submission");
        }
    }
}
