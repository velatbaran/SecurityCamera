using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityCamera.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGaleryTableClass2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 14, 15, 20, 56, 761, DateTimeKind.Local).AddTicks(4608), new Guid("13f7904b-ee99-4279-94fa-cd29d291103b") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 3, 14, 14, 27, 53, 894, DateTimeKind.Local).AddTicks(112), new Guid("e0976ce4-1912-4e33-ba4e-a2f11144a250") });
        }
    }
}
