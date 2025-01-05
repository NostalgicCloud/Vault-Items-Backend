using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLiteMinecraftApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "minecraftitems",
                columns: table => new
                {
                    UUID = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    item1 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item3 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item4 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item5 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item6 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item7 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item8 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    item9 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_minecraftitems", x => x.UUID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "minecraftitems");
        }
    }
}
