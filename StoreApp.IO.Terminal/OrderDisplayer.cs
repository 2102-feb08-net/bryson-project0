﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public class OrderDisplayer
    {
        public string GetOrderDisplay(IOrder order)
        {

            StringBuilder sb = new StringBuilder();

            string stateDisplay = order.State == OrderState.Processed ? order.OrderTime.ToString() : "In Progress";
            string customer = order?.Customer?.DisplayName() ?? "Missing Customer";
            string location = order?.StoreLocation?.Address ?? "No Location Set";

            sb.AppendLine($"======================================================================================");
            sb.AppendLine($" {stateDisplay} | {customer} | {location}");
            sb.AppendLine($"======================================================================================");
            sb.AppendLine($"         Product Name         |      Category     | Quantity |   Unit Price");
            sb.AppendLine($"--------------------------------------------------------------------------------------");
            
            foreach(var pair in order.ShoppingCartQuantity)
            {
                ISaleItem saleItem = pair.Key;
                int quantity = pair.Value;
                sb.AppendLine($"{saleItem.Product.Name, -30} | {saleItem.Product.Category,-17} | {quantity,-8} | {saleItem.UnitPrice}");
            }

            if (order.ShoppingCartQuantity.Count == 0)
                sb.AppendLine("There are currently no items in this order.");

            return sb.ToString();
        }
    }
}