using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nutrilab.DataAccess.Models.Recipes
{
    public sealed class Configuration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(2000);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany(u => u.Recipes)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.RecipeIngredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.FavouriteUsers)
                .WithOne(fr => fr.Recipe)
                .HasForeignKey(fr => fr.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
