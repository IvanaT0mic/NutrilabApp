namespace Nutrilab.Dtos.Recipes
{
    public sealed class RecipeOutgoingDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<RecipeIngredientOutgoingDto> Ingredients { get; set; }
    }
}
