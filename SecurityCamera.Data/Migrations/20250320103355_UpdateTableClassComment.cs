using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityCamera.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableClassComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Galeries_GaleryId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "GaleryId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 20, 13, 33, 54, 704, DateTimeKind.Local).AddTicks(9063), new Guid("6932051f-1ca9-49e6-93eb-3feb2294afef") });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Galeries_GaleryId",
                table: "Comments",
                column: "GaleryId",
                principalTable: "Galeries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Galeries_GaleryId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "GaleryId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 18, 14, 22, 23, 347, DateTimeKind.Local).AddTicks(9287), new Guid("67022dfe-d2c7-482e-856c-4eb4d2327a12") });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Galeries_GaleryId",
                table: "Comments",
                column: "GaleryId",
                principalTable: "Galeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
