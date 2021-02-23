using System;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A customer who can order make an order and has been added to the database.
    /// </summary>
    public class Customer : NewCustomer, ICustomer
    {
        /// <summary>
        /// The Id of the customer
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Constructor for a new customer.
        /// </summary>
        /// <param name="firstName">The first name of a customer.</param>
        /// <param name="lastName">The last name of a customer.</param>
        /// <param name="id">The ID of a customer.</param>
        public Customer(string firstName, string lastName, int id) : base(firstName, lastName)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than or equal to 1.");

            Id = id;
        }
    }
}