using StoreApp.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Product data
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Looks up a product by its name and returns it.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <returns>Returns a single product with the specified name.</returns>
        Task<IProduct> LookupProductFromName(string name);
    }
}