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
            sb.AppendLine($"         Product Name         | Quantity |   Unit Price");
            sb.AppendLine($"--------------------------------------------------------------------------------------");
            
            foreach(var pair in order.ProductQuantity)
            {
                IProduct product = pair.Key;
                int quantity = pair.Value;
                sb.AppendLine($"{product.Name.PadRight(30)} | {quantity.ToString().PadRight(8)} | {product.Price}");
            }

            return sb.ToString();
        }
    }
}
