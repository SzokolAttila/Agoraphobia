using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgoraphobiaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxEnergy",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaxHealth",
                table: "Players",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxEnergy",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Players");
        }
    }
}
