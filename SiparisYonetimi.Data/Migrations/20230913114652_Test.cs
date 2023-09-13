using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiparisYonetimi.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 9, 13, 14, 46, 52, 550, DateTimeKind.Local).AddTicks(3695), new Guid("d321ea54-e037-4152-8dc0-79f8cc3b710e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 9, 13, 14, 43, 28, 804, DateTimeKind.Local).AddTicks(6028), new Guid("f6e9f4b4-e731-4ec7-9861-fa09f4a678a6") });
        }
    }
}
