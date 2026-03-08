using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeResources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeResources_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeResources_RecipeId",
                table: "RecipeResources",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeResources");
        }
    }
}
