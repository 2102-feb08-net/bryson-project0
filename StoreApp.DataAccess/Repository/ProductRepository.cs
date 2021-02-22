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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        /// <summary>
        /// Constructs a new Product Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public ProductRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
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