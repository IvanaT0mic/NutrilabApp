using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nutrilab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ShoppingListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ShoppingLists",
                columns: new[] { "Id", "CreatedByUserId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "Nedeljna kupovina" },
                    { 2L, 4L, "Fitness obroci" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[,]
                {
                    { 1L, "Piletina", 1.5m, 1L, "kg" },
                    { 2L, "Brokoli", 2m, 1L, "kom" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "IsChecked", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[] { 3L, true, "Maslinovo ulje", 500m, 1L, "ml" });

            migrationBuilder.InsertData(
                table: "ShoppingListItems",
                columns: new[] { "Id", "Name", "Quantity", "ShoppingListId", "Unit" },
                values: new object[,]
                {
                    { 4L, "Ovsene pahuljice", 500m, 2L, "g" },
                    { 5L, "Protein u prahu", 1m, 2L, "kg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_ShoppingListId",
                table: "ShoppingListItems",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_CreatedByUserId",
                table: "ShoppingLists",
                column: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListItems");

            migrationBuilder.DropTable(
                name: "ShoppingLists");
        }
    }
}
