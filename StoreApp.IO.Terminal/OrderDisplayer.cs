using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public class OrderDisplayer
    {
        public string DisplayOrder(IOrder order)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"======================================================================================");
            sb.AppendLine($" {order.OrderTime} | {order.Customer} | {order.StoreLocation.Address}");
            sb.AppendLine($"======================================================================================");
            sb.AppendLine($"         Product Name         |      Category     | Quantity |   Unit Price");
            sb.AppendLine($"--------------------------------------------------------------------------------------");
            
            foreach(var pair in order.ShoppingCartQuantity)
            {
                ISaleItem saleItem = pair.Key;
                int quantity = pair.Value;
                sb.AppendLine($"{saleItem.Product.Name, -30} | {saleItem.Product.Category,-17} | {quantity,-8} | {saleItem.UnitPrice}");
            }

            return sb.ToString();
        }
    }
}
