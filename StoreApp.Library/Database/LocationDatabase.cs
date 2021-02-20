using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Library
{
    public class LocationDatabase
    {
        public List<Location> Locations { get; set; } = new List<Location>();

        public List<Location> LookUp(string locationName) => Locations.FindAll(l => l.Name == locationName);
    }
}