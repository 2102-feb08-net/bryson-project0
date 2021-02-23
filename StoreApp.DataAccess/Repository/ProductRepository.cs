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
    /// <summary>
    /// Repository for manipulation of Product data
    /// </summary>
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

        /// <summary>
        /// Looks up a product by its name and returns it.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <returns>Returns a single product with the specified name.</returns>
        public async Task<IProduct> LookupProductFromName(string name)
        {
            using var context = new DigitalStoreContext(Options);

            var product = await context.Products.Where(c => c.Name == name).FirstOrDefaultAsync();

            if (product is not null)
                return new Library.Model.Product(product.Name, product.Category, product.UnitPrice, product.Id);
            else
                return null;
        }

        /// <summary>
        /// Searches the product database for products that contains a portion of the search query.
        /// </summary>
        /// <param name="searchQuery">Query to search for inside product names.</param>
        /// <returns>Returns a list of products found.</returns>
        public async Task<List<IProduct>> SearchForProducts(string searchQuery)
        {
            using var context = new DigitalStoreContext(Options);

            var products = context.Products.Where(c => c.Name.Contains(searchQuery));

            return await products.Select(p => (IProduct)new Library.Model.Product(p.Name, p.Category, p.UnitPrice, p.Id)).ToListAsync();
        }
    }
}