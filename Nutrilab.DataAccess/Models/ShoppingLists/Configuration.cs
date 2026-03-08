using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.DataAccess.Models.ShoppingLists
{
    public sealed class Configuration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.ToTable("ShoppingLists");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Items)
                .WithOne(i => i.ShoppingList)
                .HasForeignKey(i => i.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(Seed.Data);
        }
    }
}
