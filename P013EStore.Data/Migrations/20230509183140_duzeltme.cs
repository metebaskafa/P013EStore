using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P013EStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class duzeltme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 21, 31, 40, 470, DateTimeKind.Local).AddTicks(8171));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Image",
                table: "Categories",
                type: "int",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 5, 2, 22, 44, 50, 835, DateTimeKind.Local).AddTicks(1886));
        }
    }
}
