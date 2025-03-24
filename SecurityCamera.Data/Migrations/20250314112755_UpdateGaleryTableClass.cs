using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityCamera.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGaleryTableClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Galeries");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 14, 14, 27, 53, 894, DateTimeKind.Local).AddTicks(112), new Guid("e0976ce4-1912-4e33-ba4e-a2f11144a250") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Galeries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 13, 14, 56, 0, 483, DateTimeKind.Local).AddTicks(4334), new Guid("a1e546a1-b2af-43b2-abc4-d72ed470c5dc") });
        }
    }
}
