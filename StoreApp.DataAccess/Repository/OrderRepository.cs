using Microsoft.EntityFrameworkCore;
using StoreApp.Library;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Order data
    /// </summary>
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        /// <summary>
        /// Constructs a new Order Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public OrderRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
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

            var productIds = order.ShoppingCartQuantity.Keys.Select(s => s.Id).ToList();
            var foundProducts = await context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            if (productIds.Count != foundProducts.Count)
                throw new OrderException("One or more Products in cart did not exist in the database.");

            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                // TODO: Replace with proper authentication and unique identification using a Login System
                CustomerId = order.Customer.Id,
                DateProcessed = DateTime.Now,
                OrderLines = new List<OrderLine>(),
                StoreLocationId = order.StoreLocation.Id
            };

            await AddProductsToOrder(order, context, foundProducts, purchaseOrder);

            await context.PurchaseOrders.AddAsync(purchaseOrder);

            await context.SaveChangesAsync();
        }

        private static async Task AddProductsToOrder(IOrder order, DigitalStoreContext context, List<Product> foundProducts, PurchaseOrder purchaseOrder)
        {
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

                if (inventory is null)
                    throw new OrderException($"Store location does not contain the product '{product.Name}' in its inventory.");

                if (inventory.Quantity >= quantity)
                    inventory.Quantity -= quantity;
                else
                    throw new OrderException($"The store location only has {inventory.Quantity} of '{inventory.Product.Name}' in stock, but the order is requesting to order {quantity} of the product.");

                purchaseOrder.OrderLines.Add(new OrderLine()
                {
                    Quantity = quantity,
                    Product = foundProduct,
                    PurchaseOrder = purchaseOrder,
                    PurchaseUnitPrice = product.UnitPrice
                });
            }
        }

        /// <summary>
        /// Retrieves a list of all of the orders from the specified customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns a list of all of the orders found</returns>
        public async Task<List<IReadOnlyOrder>> GetOrdersFromCustomer(Library.Model.ICustomer customer)
        {
            using var context = new DigitalStoreContext(Options);

            var purchaseOrders = await context.PurchaseOrders
                .Include(p => p.Customer)
                .Include(p => p.OrderLines)
                .ThenInclude(o => o.Product)
                .Include(p => p.StoreLocation)
                .ThenInclude(s => s.Address)
                .Where(p => p.Customer.Id == customer.Id).ToListAsync();

            var orderList = ConvertPurchaseOrderToIOrders(purchaseOrders);

            return orderList;
        }

        /// <summary>
        /// Retrieves a list of all of the orders from a specified location.
        /// </summary>
        /// <param name="locationName">The name of the location.</param>
        /// <returns>Returns a list of all of the orders found.</returns>
        public async Task<List<IReadOnlyOrder>> GetOrdersFromLocation(string locationName)
        {
            using var context = new DigitalStoreContext(Options);

            var purchaseOrders = await context.PurchaseOrders
                .Include(p => p.Customer)
                .Include(p => p.OrderLines)
                .ThenInclude(o => o.Product)
                .Include(p => p.StoreLocation)
                .ThenInclude(s => s.Address)
                .Where(p => p.StoreLocation.Name == locationName).ToListAsync();

            var orderList = ConvertPurchaseOrderToIOrders(purchaseOrders);
            return orderList;
        }

        private static List<IReadOnlyOrder> ConvertPurchaseOrderToIOrders(List<PurchaseOrder> purchaseOrders)
        {
            List<IReadOnlyOrder> orders = new List<IReadOnlyOrder>();
            foreach (var purchase in purchaseOrders)
            {
                ICustomer customer = new Library.Model.Customer(purchase.Customer.FirstName, purchase.Customer.LastName, purchase.CustomerId);
                ILocation location = new Location(
                    name: purchase.StoreLocation.Name,
                    address: purchase.StoreLocation.Address.Print(),
                    inventory: new Dictionary<IProduct, int>(),
                    id: purchase.StoreLocation.Id
                );

                Dictionary<IProduct, int> productQuantities = new Dictionary<IProduct, int>();
                foreach (var line in purchase.OrderLines)
                {
                    IProduct product = new Library.Model.Product(line.Product.Name, line.Product.Category, line.PurchaseUnitPrice, line.Product.Id);
                    productQuantities.Add(product, line.Quantity);
                }

                var order = new ProcessedOrder(customer, location, productQuantities, purchase.DateProcessed, purchase.Id);
                orders.Add(order);
            }

            return orders;
        }
    }
}