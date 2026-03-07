using Nutrilab.DataAccess.Models.FavouriteRecipes;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.DataAccess.Models.UserRoles;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.Users
{
    public class User : IUser
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<Recipe> Recipes { get; set; }

        public List<FavouriteRecipe> FavouriteRecipes { get; set; }

        List<IUserRole> IUser.UserRoles => UserRoles.ToList<IUserRole>();

        List<IRecipe> IUser.Recipes => Recipes.ToList<IRecipe>();

        List<IFavouriteRecipe> IUser.FavouriteRecipes => FavouriteRecipes.ToList<IFavouriteRecipe>();
    }
}
