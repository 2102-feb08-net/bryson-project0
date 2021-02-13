using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IOrder
    {
        ICustomer Customer { get; }

        Location StoreLocation { get; }

        IReadOnlyDictionary<IProduct, int> ProductQuantity { get; }
    
        DateTime OrderTime { get; }
    }
}
