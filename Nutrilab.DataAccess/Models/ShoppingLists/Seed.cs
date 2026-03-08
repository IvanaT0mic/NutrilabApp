namespace Nutrilab.DataAccess.Models.ShoppingLists
{
    public static class Seed
    {
        public static ShoppingList[] Data => new[]
        {
            new ShoppingList
            {
                Id = 1,
                Name = "Nedeljna kupovina",
                CreatedByUserId = 1
            },
            new ShoppingList
            {
                Id = 2,
                Name = "Fitness obroci",
                CreatedByUserId = 4
            }
        };
    }
}
