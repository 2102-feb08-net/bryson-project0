using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public record SaleItem : ISaleItem
    {
        public IProduct Product { get; init; }

        public decimal UnitPrice { get; init; }
    }
}