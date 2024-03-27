using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRSWebApi.Migrations
{
    /// <inheritdoc />
    public partial class SaltIsADDed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                schema: "Auth",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                schema: "Auth",
                table: "Users");
        }
    }
}
