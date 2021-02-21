using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public interface IOrder : IReadOnlyOrder
    {
        AttemptResult TryAddProductToOrder(IProduct product, int quantity);
    }
}