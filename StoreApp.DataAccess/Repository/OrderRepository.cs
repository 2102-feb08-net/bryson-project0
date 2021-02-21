using Microsoft.EntityFrameworkCore;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

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

        private List<IReadOnlyOrder> ConvertPurchaseOrderToIOrders(List<PurchaseOrder> purchaseOrders)
        {
            List<IReadOnlyOrder> orders = new List<IReadOnlyOrder>();
            foreach (var purchase in purchaseOrders)
            {
                ICustomer customer = new Library.Model.Customer(purchase.Customer.FirstName, purchase.Customer.LastName, purchase.CustomerId);
                ILocation location = new Location()
                {
                    Name = purchase.StoreLocation.Name,
                    Address = purchase.StoreLocation.Address.Print(),
                    Id = purchase.StoreLocation.Id
                };

                Dictionary<IProduct, int> productQuantities = new Dictionary<IProduct, int>();
                foreach (var line in purchase.OrderLines)
                {
                    IProduct product = new ProductData(line.Product.Name, line.Product.Category, line.Product.UnitPrice);
                    productQuantities.Add(product, line.Quantity);
                }

                var order = new ProcessedOrder(customer, location, productQuantities, purchase.DateProcessed, purchase.Id);
                orders.Add(order);
            }

            return orders;
        }
    }
}