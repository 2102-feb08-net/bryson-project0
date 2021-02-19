using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public class LocationMenuHelper
    {
        public static Location LookUpLocation(IIOController io, LocationDatabase database)
        {
            io.Output.Write("Enter the name of the location:");
            string locationName = io.Input.ReadInput();

            List<Location> locations = database.LookUp(locationName);

            if (locations.Count == 0)
            {
                io.Output.Write($"No location found with the name '{locationName}'");
                return null;
            }

            if (locations.Count > 1)
            {
                io.Output.Write($"More than one location was found with the name '{locationName}'. Returning first one found.");
            }

            return locations[0];
        }
    }
}
