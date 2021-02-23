using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class NewCustomer : INewCustomer
    {
        /// <summary>
        /// The first name of the customer.
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// Gets the customers full name.
        /// </summary>
        /// <returns>Returns the customer with their name formatted as their full name.</returns>
        public override string ToString() => $"{FirstName} {LastName}";

        /// <summary>
        /// Constructor for a new customer.
        /// </summary>
        /// <param name="firstName">The first name of a customer.</param>
        /// <param name="lastName">The last name of a customer.</param>
        /// <param name="id">The ID of a customer.</param>
        public NewCustomer(string firstName, string lastName)
        {
            if (firstName is null)
                throw new ArgumentNullException(nameof(firstName), message: "A customer must have a first name that is not null.");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException(paramName: nameof(firstName), message: "A customer must have a first name that is not null or empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                lastName = null;

            FirstName = firstName.Trim();
            LastName = lastName?.Trim(); // A possible null, or empty last name is by design.
        }
    }
}