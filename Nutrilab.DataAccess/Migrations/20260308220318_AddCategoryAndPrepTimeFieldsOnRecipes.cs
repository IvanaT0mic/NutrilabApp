using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutrilab.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndPrepTimeFieldsOnRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DifficultyLvl",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealCategory",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTimeMinutes",
                table: "Recipes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLvl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MealCategory",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationTimeMinutes",
                table: "Recipes");
        }
    }
}
