using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IShoppingList
    {
        long Id { get; }

        string Name { get; }

        long CreatedByUserId { get; }

        IUser CreatedByUser { get; }

        List<IShoppingListItem> Items { get; }
    }
}
