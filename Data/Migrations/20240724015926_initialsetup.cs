using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BJTracker.Data.Migrations;

/// <inheritdoc />
public partial class initialsetup : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Session",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Casino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Date = table.Column<int>(type: "int", nullable: false),
                Result = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Session", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Session");
    }
}
