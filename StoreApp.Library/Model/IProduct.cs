using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A product that can be added to an order
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The category of the product.
        /// </summary>
        string Category { get; }

        /// <summary>
        /// The unit price of the product in USD.
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// The ID of the product.
        /// </summary>
        int Id { get; }
    }
}