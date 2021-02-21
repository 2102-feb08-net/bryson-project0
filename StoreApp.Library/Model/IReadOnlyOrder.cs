using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public interface IReadOnlyOrder
    {
        ICustomer Customer { get; }

        ILocation StoreLocation { get; }

        IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity { get; }

        DateTimeOffset? OrderTime { get; }

        int? Id { get; }
    }
}