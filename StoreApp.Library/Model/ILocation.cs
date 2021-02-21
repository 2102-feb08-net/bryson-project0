using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public interface ILocation
    {
        string Name { get; init; }
        string Address { get; init; }
        int Id { get; init; }

        Dictionary<IProduct, int> Inventory { get; init; }

        bool IsProductAvailable(IProduct product, int quantity);

        int GetAvailableStock(IProduct product);
    }
}