using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataAccess
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreLocationId { get; set; }
        public DateTimeOffset DateProcessed { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual StoreLocation StoreLocation { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
