using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anzen.Migrations
{
    /// <inheritdoc />
    public partial class StatusFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Status_StatusId",
                table: "Submission");

            migrationBuilder.DropIndex(
                name: "IX_Submission_StatusId",
                table: "Submission");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Submission");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Submission",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Status_Id",
                table: "Submission",
                column: "Id",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Status_Id",
                table: "Submission");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Submission",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Submission",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Submission_StatusId",
                table: "Submission",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Status_StatusId",
                table: "Submission",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
