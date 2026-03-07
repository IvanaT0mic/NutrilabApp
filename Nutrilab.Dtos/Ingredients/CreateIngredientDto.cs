namespace Nutrilab.Dtos.Ingredients
{
    public record CreateIngredientDto
    {
        public string Name { get; init; }
        public string Unit { get; init; }
    }
}
