using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public interface ISaleItem
    {
        IProduct Product { get; }

        decimal UnitPrice { get; }
    }
}
