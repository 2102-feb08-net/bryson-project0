using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Library
{
    public static class OrderProcessor
    {
        public static decimal CalculateTotalPrice(this IReadOnlyOrder order)
        {
            decimal total = 0;
            foreach (var pair in order.ShoppingCartQuantity)
            {
                IProduct saleItem = pair.Key;
                int quantity = pair.Value;
                total += saleItem.UnitPrice * quantity;
            }
            return total;
        }
    }
}