namespace Nutrilab.Dtos.Recipes.RecipeDetailOutgoingDtos
{
    public sealed class RecipeDetailOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<RecipeIngredientOutgoingDto> Ingredients { get; set; }
        public string ImageBase64 { get; set; }
    }
}
