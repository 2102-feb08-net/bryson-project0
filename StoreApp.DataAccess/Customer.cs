using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
