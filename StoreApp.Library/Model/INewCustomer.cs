using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A customer that has just been created and doesn't exist in the database.
    /// </summary>
    public interface INewCustomer
    {
        /// <summary>
        /// The first name of the customer.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gives the full display name of the customer.
        /// </summary>
        /// <returns>Returns the full name of the customer.</returns>
        public string DisplayName() => $"{FirstName} {LastName}";
    }
}