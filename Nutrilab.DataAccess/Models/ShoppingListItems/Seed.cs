using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.DataAccess.Models.ShoppingListItems
{
    public static class Seed
    {
        public static ShoppingListItem[] Data => new[]
        {
            new ShoppingListItem
            {
                Id = 1,
                Name = "Piletina",
                Quantity = 1.5m,
                Unit = "kg",
                IsChecked = false,
                ShoppingListId = 1
            },
            new ShoppingListItem
            {
                Id = 2,
                Name = "Brokoli",
                Quantity = 2,
                Unit = "kom",
                IsChecked = false,
                ShoppingListId = 1
            },
            new ShoppingListItem
            {
                Id = 3,
                Name = "Maslinovo ulje",
                Quantity = 500,
                Unit = "ml",
                IsChecked = true,
                ShoppingListId = 1
            },
            new ShoppingListItem
            {
                Id = 4,
                Name = "Ovsene pahuljice",
                Quantity = 500,
                Unit = "g",
                IsChecked = false,
                ShoppingListId = 2
            },
            new ShoppingListItem
            {
                Id = 5,
                Name = "Protein u prahu",
                Quantity = 1,
                Unit = "kg",
                IsChecked = false,
                ShoppingListId = 2
            }
        };
    }
}
