using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DataAccess.Repository;
using StoreApp.Library;
using StoreApp.Library.Model;

namespace StoreApp.IO.Terminal
{
    public class LocationMenuHelper
    {
        public static async Task<ILocation> LookUpLocation(IIOController io, MainDatabase database)
        {
            io.Output.Write("Enter the name of the location:");
            string locationName = io.Input.ReadInput();

            ILocationRepository repo = database.LocationRepository;

            ILocation location = await repo.LookUpLocationByNameAsync(locationName);

            if (location is null)
            {
                io.Output.Write($"No location found with the name '{locationName}'");
                return null;
            }

            return location;
        }
    }
}