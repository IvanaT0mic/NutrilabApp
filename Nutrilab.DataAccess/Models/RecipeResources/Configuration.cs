namespace Nutrilab.DataAccess.Models.RecipeResources
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Nutrilab.DataAccess.Models.RecipeResources
    {
        public sealed class Configuration : IEntityTypeConfiguration<RecipeResource>
        {
            public void Configure(EntityTypeBuilder<RecipeResource> builder)
            {
                builder.ToTable("RecipeResources");

                builder.HasKey(x => x.Id);

                builder.Property(x => x.Base64)
                    .IsRequired();

                builder.HasOne(x => x.Recipe)
                    .WithMany(r => r.Resources)
                    .HasForeignKey(x => x.RecipeId)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        }
    }
}
