using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using StoreApp.Library.Model;
using StoreApp.Library;

namespace StoreApp.DataAccess.Repository
{
    public class ProductRepository : BaseRepository
    {
        /// <summary>
        /// Constructor for a new Product Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public ProductRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        /// <summary>
        /// Sends and process an order to the database.
        /// </summary>
        /// <param name="order">The order to process</param>
        public async Task SendOrderTransaction(IOrder order)
        {
            using var context = new DigitalStoreContext(Options);

            if (order.ShoppingCartQuantity.Count == 0)
                throw new OrderException("Cannot submit order with no products in cart.");

            var productNames = order.ShoppingCartQuantity.Keys.Select(s => s.Name).ToList();
            var foundProducts = await context.Products.Where(p => productNames.Contains(p.Name)).ToListAsync();

            if (productNames.Count != foundProducts.Count)
                throw new OrderException("One or more Products in cart did not exist in the database.");

            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                // TODO: Replace with proper authentication and unique identification using a Login System
                CustomerId = order.Customer.Id,
                DateProcessed = DateTime.Now,
                OrderLines = new List<OrderLine>(),
                StoreLocationId = order.StoreLocation.Id
            };

            var inventories = await context.Inventories
                .Where(i => i.StoreId == purchaseOrder.StoreLocationId).ToListAsync();

            foreach (var productQuantity in order.ShoppingCartQuantity)
            {
                var product = productQuantity.Key;
                int quantity = productQuantity.Value;

                if (quantity <= 0)
                    throw new OrderException("Cannot order products with a quantity less than or equal to 0.");

                Product foundProduct = foundProducts.Find(p => p.Name == product.Name);

                var inventory = inventories.Where(i => i.ProductId == foundProduct.Id).FirstOrDefault();

                if (inventory == null)
                    throw new OrderException($"Store location does not contain the product '{product.Name}' in its inventory.");

                if (inventory.Quantity >= quantity)
                    inventory.Quantity -= quantity;
                else
                    throw new OrderException($"The store location only has {inventory.Quantity} of '{inventory.Product.Name}' in stock, but the order is requesting to order {quantity} of the product.");

                purchaseOrder.OrderLines.Add(new OrderLine()
                {
                    Quantity = quantity,
                    Product = foundProduct,
                    PurchaseOrder = purchaseOrder
                });
            }

            await context.PurchaseOrders.AddAsync(purchaseOrder);

            await context.SaveChangesAsync();
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