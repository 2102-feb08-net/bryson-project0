using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataAccess
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            Inventories = new HashSet<Inventory>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
