using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class MainDatabase
    {
        public CustomerDatabase CustomerDatabase { get; set; } = new CustomerDatabase();

        public ProductDatabase ProductDatabase { get; set; } = new ProductDatabase();

        public LocationDatabase LocationDatabase { get; set; } = new LocationDatabase();

        public OrderHistory OrderHistory { get; set; } = new OrderHistory();

        public string ConnectionString { get; set; }
    }
}
