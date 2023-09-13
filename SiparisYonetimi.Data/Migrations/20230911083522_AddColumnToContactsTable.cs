using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiparisYonetimi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToContactsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 9, 11, 11, 35, 22, 236, DateTimeKind.Local).AddTicks(1566));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Contacts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 9, 8, 13, 55, 54, 608, DateTimeKind.Local).AddTicks(2108));
        }
    }
}
