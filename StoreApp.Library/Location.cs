using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class Location : IIdentifiable
    {
        public string Address { get; }

        public Dictionary<IProduct, int> Inventory = new Dictionary<IProduct, int>();


        public Guid ID { get; }

    }
}
