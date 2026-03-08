using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.DataAccess.Models.ShoppingListItems
{
    public sealed class Configuration : IEntityTypeConfiguration<ShoppingListItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            builder.ToTable("ShoppingListItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Quantity)
                .HasPrecision(10, 2);

            builder.Property(x => x.Unit)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.IsChecked)
                .HasDefaultValue(false);

            builder.HasData(Seed.Data);
        }
    }
}
