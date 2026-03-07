using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.FavouriteRecipes
{
    public class FavouriteRecipe : IFavouriteRecipe
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public long RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public DateTime CreatedAt { get; set; }

        IUser IFavouriteRecipe.User => User;

        IRecipe IFavouriteRecipe.Recipe => Recipe;
    }
}
