using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nutrilab.DataAccess.Models.RecipeIngredients
{
    public sealed class Configuration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable("RecipeIngredients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired()
                .HasPrecision(10, 2);

            builder.HasOne(x => x.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(x => x.IngredientId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
