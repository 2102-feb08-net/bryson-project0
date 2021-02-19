using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public interface IOrder
    {
        ICustomer Customer { get; }

        Location StoreLocation { get; }

        IReadOnlyDictionary<ISaleItem, int> ShoppingCartQuantity { get; }
    
        DateTimeOffset? OrderTime { get; }

        decimal TotalPrice { get; }

        OrderState State { get; }

        Guid ID { get; }

        void ProcessOrder();
    }
}
