using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApp.Library;

namespace StoreApp.DataAccess.Repository
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        public async Task TryOrderTransaction(Library.Model.IOrder order)
        {
            using var context = new DigitalStoreContext();

            List<OrderLine> orderLines = new List<OrderLine>();
            var productNames = order.ShoppingCartQuantity.Keys.Select(s => s.Product.Name).ToList();
            var foundProducts = await context.Products.Where(p => productNames.Contains(p.Name)).ToListAsync();
            foreach (var productQuantity in order.ShoppingCartQuantity)
            {
                var product = productQuantity.Key;
                int quantity = productQuantity.Value;
                orderLines.Add(new OrderLine()
                {
                    Quantity = quantity,
                    Product = foundProducts.Find(p => p.Name == product.Product.Name)
                });
            }

            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                Id = await GenerateNextIdAsync(context.PurchaseOrders),
                OrderLines = orderLines,

                // TODO: Replace with proper authentication and unique identification using a Login System
                CustomerId = order.Customer.Id,
                DateProcessed = DateTime.Now
            };

            await context.PurchaseOrders.AddAsync(purchaseOrder);

            await context.SaveChangesAsync();
        }

        public async Task<ProductData> LookupProductFromName(string name)
        {
            using var context = new DigitalStoreContext(Options);

            var product = await context.Products.Where(c => c.Name == name).FirstOrDefaultAsync();

            return new ProductData(product.Name, product.Category);
        }

        public async Task<List<ProductData>> SearchForProducts(string searchQuery)
        {
            using var context = new DigitalStoreContext(Options);

            var products = context.Products.Where(c => c.Name.Contains(searchQuery));

            return await products.Select(p => new ProductData(p.Name, p.Category)).ToListAsync();
        }
    }
}