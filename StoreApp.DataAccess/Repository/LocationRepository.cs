using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.DataAccess.Repository
{
    public class LocationRepository : BaseRepository
    {
        public LocationRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        public async Task<Library.Model.Location> LookUpLocationByNameAsync(string name)
        {
            using var context = new DigitalStoreContext(Options);

            var storeLocation = await context.StoreLocations
                .Include(s => s.Inventories)
                .ThenInclude(i => i.Product)
                .Include(s => s.Address)
                .FirstAsync(s => s.Name == name);
            var inventoryPairs = storeLocation.Inventories.Select(
                i => new KeyValuePair<Library.Model.IProduct, int>(
                    new Library.Model.ProductData(i.Product.Name, i.Product.Category),
                    i.Quantity)).ToList();

            var inventoryDictionary = inventoryPairs.ToDictionary((keyItem) => (keyItem).Key, (valueItem) => valueItem.Value);

            Library.Model.Location location = new()
            {
                Name = storeLocation.Name,
                Address = PrintAddress(storeLocation.Address),
                Inventory = inventoryDictionary
            };

            return location;
        }

        private string PrintAddress(Address address)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(address.Address1);

            if (address.Address2 != null)
                sb.AppendLine(address.Address2);
            sb.AppendLine($"{address.City}, {address.State} {address.ZipCode}");
            sb.AppendLine(address.Country);
            return sb.ToString();
        }
    }
}