using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgoraphobiaAPI.Migrations
{
    /// <inheritdoc />
    public partial class PlayerIncluded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Sanity = table.Column<double>(type: "float", nullable: false),
                    Health = table.Column<double>(type: "float", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<double>(type: "float", nullable: false),
                    Defense = table.Column<double>(type: "float", nullable: false),
                    DreamCoins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
