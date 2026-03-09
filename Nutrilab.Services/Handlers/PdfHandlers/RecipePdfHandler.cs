using Microsoft.Reporting.NETCore;
using Nutrilab.Repositories;
using Nutrilab.Shared.Enums;
using Nutrilab.Shared.Models.Exceptions;
using System.Data;

namespace Nutrilab.Services.Handlers.PdfHandlers
{
    public class RecipePdfHandler(IRecipeRepository _recipeRepository) : IPdfHandler
    {
        public PdfReportType ReportType => PdfReportType.Recipe;

        public async Task<byte[]> GenerateAsync(long id)
        {
            var recipe = await _recipeRepository.GetByIdExtendedAsync(id);
            if (recipe == null) throw new NotFoundException($"Recipe {id} not found");

            var recipeTable = new DataTable();
            recipeTable.Columns.Add("Name");
            recipeTable.Columns.Add("Description");
            recipeTable.Columns.Add("PreparationTimeMinutes");
            recipeTable.Columns.Add("DifficultyLvl");
            recipeTable.Columns.Add("MealCategory");
            recipeTable.Rows.Add(
                recipe.Name,
                recipe.Description ?? "",
                recipe.PreparationTimeMinutes?.ToString() ?? "N/A",
                recipe.DifficultyLvl?.ToString() ?? "N/A",
                recipe.MealCategory?.ToString() ?? "N/A"
            );

            var ingredientsTable = new DataTable();
            ingredientsTable.Columns.Add("IngredientName");
            ingredientsTable.Columns.Add("Quantity");
            ingredientsTable.Columns.Add("Unit");
            foreach (var ing in recipe.RecipeIngredients)
            {
                ingredientsTable.Rows.Add(
                    ing.Ingredient.Name,
                    ing.Quantity,
                    ing.Ingredient.Unit ?? ""
                );
            }

            var assembly = typeof(RecipePdfHandler).Assembly;
            var resourceName = "Nutrilab.Services.Handlers.PdfHandlers.Reports.RecipeReport.rdlc";
            using var stream = assembly.GetManifestResourceStream(resourceName);

            using var report = new LocalReport();
            report.LoadReportDefinition(stream);
            report.DataSources.Add(new ReportDataSource("RecipeDataSet", recipeTable));
            report.DataSources.Add(new ReportDataSource("IngredientsDataSet", ingredientsTable));
            return report.Render("PDF");
        }
    }
}
