using StoreApp.Library.Model;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets the location by a given name.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <returns>Returns the location with the given name.</returns>
        Task<Location> LookUpLocationByNameAsync(string name);
    }
}