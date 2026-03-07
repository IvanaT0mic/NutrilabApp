using Nutrilab.DataAccess.Models.FavouriteRecipes;
using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.Recipes
{
    public class Recipe : IRecipe
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public long CreatedByUserId { get; set; }

        public User CreatedByUser { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }

        public List<FavouriteRecipe> FavouriteUsers { get; set; }

        IUser IRecipe.CreatedByUser => CreatedByUser;

        List<IRecipeIngredient> IRecipe.RecipeIngredients => RecipeIngredients.ToList<IRecipeIngredient>();

        List<IFavouriteRecipe> IRecipe.FavouriteUsers => FavouriteUsers.ToList<IFavouriteRecipe>();
    }
}
