using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityCamera.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClassAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "CreatedDate", "UserGuid" },
                values: new object[] { "Örnek mah. Hastane sok. Merkez/Kars", new DateTime(2025, 3, 21, 13, 47, 40, 556, DateTimeKind.Local).AddTicks(2947), new Guid("a9a4e8c6-a737-4d01-85f3-f665ef329bf8") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AppUsers");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 20, 13, 33, 54, 704, DateTimeKind.Local).AddTicks(9063), new Guid("6932051f-1ca9-49e6-93eb-3feb2294afef") });
        }
    }
}
