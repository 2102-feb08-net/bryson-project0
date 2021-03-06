﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;

namespace StoreApp.IO.Terminal
{
    public class OrderDisplayer
    {
        public static string GetOrderDisplay(IReadOnlyOrder order)
        {
            StringBuilder sb = new StringBuilder();

            string stateDisplay = order.Id is not null && order.OrderTime is not null ? $"{order.OrderTime} | Order# {order.Id}" : "Order In Progress";
            string customer = order?.Customer?.DisplayName() ?? "Missing Customer";
            string location = order?.StoreLocation?.Name ?? "No Location Set";

            string overviewData = $"{stateDisplay} | {customer} | {location}";

            sb.AppendLine($"================================================================================================");
            sb.AppendLine($"= {overviewData,-92} =");
            sb.AppendLine($"================================================================================================");
            sb.AppendLine($"\t|        Product Name         |      Category     | Quantity |   Unit Price            |");
            sb.AppendLine($"\t|--------------------------------------------------------------------------------------|");

            foreach (var pair in order.ShoppingCartQuantity)
            {
                IProduct product = pair.Key;
                int quantity = pair.Value;
                sb.AppendLine($"\t|{product.Name,-28} | {product.Category,-17} | {quantity,-8} | ${product.UnitPrice,-23:0.##}|");
            }

            if (order.ShoppingCartQuantity.Count == 0)
                sb.AppendLine("\t| There are currently no items in this order.                                          |");
            sb.AppendLine($"\t|--------------------------------------------------------------------------------------|");
            sb.AppendLine($"\t|Total: ${OrderProcessor.CalculateTotalPrice(order),-78:0.##}|");
            sb.AppendLine($"\t----------------------------------------------------------------------------------------");

            return sb.ToString();
        }

        public static IEnumerable<string> GetBatchOrderDisplay(IEnumerable<IReadOnlyOrder> orders)
        {
            foreach (var order in orders)
            {
                yield return GetOrderDisplay(order);
            }
        }
    }
}