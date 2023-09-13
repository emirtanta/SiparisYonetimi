using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiparisYonetimi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnUserGuidToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 9, 11, 14, 36, 15, 408, DateTimeKind.Local).AddTicks(4613), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 9, 11, 11, 35, 22, 236, DateTimeKind.Local).AddTicks(1566));
        }
    }
}
