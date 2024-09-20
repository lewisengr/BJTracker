using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Result",
                table: "Session",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Session"
                );

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Session",
                type: "date",
                nullable: false,
                defaultValueSql: "'1/1/2024'"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "Session",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Date",
                table: "Session",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
