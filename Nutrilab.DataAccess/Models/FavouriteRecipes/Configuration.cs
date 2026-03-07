using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nutrilab.DataAccess.Models.FavouriteRecipes
{
    public sealed class Configuration : IEntityTypeConfiguration<FavouriteRecipe>
    {
        public void Configure(EntityTypeBuilder<FavouriteRecipe> builder)
        {
            builder.ToTable("FavouriteRecipes");

            builder.HasKey(x => new { x.UserId, x.RecipeId });

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(u => u.FavouriteRecipes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Recipe)
                .WithMany(r => r.FavouriteUsers)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
