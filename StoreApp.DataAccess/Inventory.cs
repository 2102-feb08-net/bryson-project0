using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataAccess
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual StoreLocation Store { get; set; }
    }
}
