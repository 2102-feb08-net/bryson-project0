﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApp.Library.Model;

namespace StoreApp.DataAccess.Repository
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        public async Task<bool> TryOrderTransaction(IOrder order)
        {
            using var context = new DigitalStoreContext(Options);

            var productNames = order.ShoppingCartQuantity.Keys.Select(s => s.Name).ToList();
            var foundProducts = await context.Products.Where(p => productNames.Contains(p.Name)).ToListAsync();

            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                // TODO: Replace with proper authentication and unique identification using a Login System
                CustomerId = order.Customer.Id,
                DateProcessed = DateTime.Now,
                OrderLines = new List<OrderLine>(),
                StoreLocationId = order.StoreLocation.Id
            };

            foreach (var productQuantity in order.ShoppingCartQuantity)
            {
                var product = productQuantity.Key;
                int quantity = productQuantity.Value;
                purchaseOrder.OrderLines.Add(new OrderLine()
                {
                    Quantity = quantity,
                    Product = foundProducts.Find(p => p.Name == product.Name),
                    PurchaseOrder = purchaseOrder
                });
            }

            await context.PurchaseOrders.AddAsync(purchaseOrder);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IProduct> LookupProductFromName(string name)
        {
            using var context = new DigitalStoreContext(Options);

            var product = await context.Products.Where(c => c.Name == name).FirstOrDefaultAsync();

            if (product != null)
                return new ProductData(product.Name, product.Category, product.UnitPrice);
            else
                return null;
        }

        public async Task<List<IProduct>> SearchForProducts(string searchQuery)
        {
            using var context = new DigitalStoreContext(Options);

            var products = context.Products.Where(c => c.Name.Contains(searchQuery));

            return await products.Select(p => (IProduct)new ProductData(p.Name, p.Category, p.UnitPrice)).ToListAsync();
        }
    }
}