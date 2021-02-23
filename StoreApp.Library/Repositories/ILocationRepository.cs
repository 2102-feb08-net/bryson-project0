using StoreApp.Library.Model;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Location data
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Gets the location by a given name.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <returns>Returns the location with the given name.</returns>
        Task<ILocation> LookUpLocationByNameAsync(string name);
    }
}