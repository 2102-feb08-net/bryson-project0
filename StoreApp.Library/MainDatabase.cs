using StoreApp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Provides database access to each of the repositories.
    /// </summary>
    public class MainDatabase
    {
        /// <summary>
        /// The repository with access to all of the products in the database
        /// </summary>
        public IProductRepository ProductRepository { get; set; }

        /// <summary>
        /// The repository with access to all of the orders in the database
        /// </summary>
        public IOrderRepository OrderRepository { get; set; }

        /// <summary>
        /// The repository with access to all of the store locations in the database
        /// </summary>
        public ILocationRepository LocationRepository { get; set; }

        /// <summary>
        /// The repository with access to all of the customers in the database
        /// </summary>
        public ICustomerRepository CustomerRepository { get; set; }
    }
}