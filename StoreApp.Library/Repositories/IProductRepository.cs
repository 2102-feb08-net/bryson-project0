using StoreApp.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public interface IProductRepository
    {
        Task<IProduct> LookupProductFromName(string name);
        Task<List<IProduct>> SearchForProducts(string searchQuery);
    }
}