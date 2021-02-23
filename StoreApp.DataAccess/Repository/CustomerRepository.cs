using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    /// <summary>
    /// Repository for manipulation of Customer data
    /// </summary>
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        /// <summary>
        /// Constructs a new Customer Repository
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="logger">The logger to log the connection.</param>
        public CustomerRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        /// <summary>
        /// Creates and sends a request to the server to get the customer entry with the specified first and last name
        /// </summary>
        /// <param name="firstName">The first name of the customer</param>
        /// <param name="lastName">The last name of the customer</param>
        /// <returns>Returns a list of customers as some customers may have the same name</returns>
        public async Task<List<Library.Model.Customer>> LookUpCustomersByNameAsync(string firstName, string lastName)
        {
            using var context = new DigitalStoreContext(Options);

            var query = GetCustomersFromName(context, firstName, lastName);

            var customers = await query.ToListAsync();
            return customers.Select(c => new Library.Model.Customer(c.FirstName, c.LastName, c.Id)).ToList();
        }

        /// <summary>
        /// Searches the database for customers that have either their first or last name contain the search query
        /// </summary>
        /// <param name="nameQuery">The query to search for in the customers' full names</param>
        /// <returns>Returns a list of customers as some customers contain the query</returns>
        public async Task<List<Library.Model.ICustomer>> SearchCustomersAsync(string nameQuery)
        {
            using var context = new DigitalStoreContext(Options);

            if (string.IsNullOrWhiteSpace(nameQuery))
                throw new ArgumentException("Search query cannot be empty or null");

            var customers = await context.Customers.Where(c => c.FirstName.Contains(nameQuery) || (!ReferenceEquals(c.LastName, null) && c.LastName.Contains(nameQuery))).ToListAsync();
            return customers.Select(c => (Library.Model.ICustomer)new Library.Model.Customer(c.FirstName, c.LastName, c.Id)).ToList();
        }

        /// <summary>
        /// Creates a customer and adds it into the database. It does not check for duplicates before creating it.
        /// </summary>
        /// <param name="firstName">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        /// <returns>Returns whether it was successful in adding the customer to the database</returns>
        public async Task<bool> TryCreateCustomerAsync(string firstName, string lastName)
        {
            using var context = new DigitalStoreContext(Options);

            bool lastNameIsEmpty = string.IsNullOrWhiteSpace(lastName);

            await context.Customers.AddAsync(new Customer()
            {
                FirstName = firstName.Trim(),
                LastName = lastNameIsEmpty ? null : lastName.Trim()
            }
            );

            return await context.SaveChangesAsync() > 0;
        }

        private static IQueryable<Customer> GetCustomersFromName(DigitalStoreContext context, string firstName, string lastName)
        {
            string trimmedFirst = firstName.Trim();
            string trimmedLast = string.IsNullOrWhiteSpace(lastName) ? null : lastName.Trim();

            var customerQuery = context.Customers.Where(c => c.FirstName == trimmedFirst && c.LastName == trimmedLast);
            return customerQuery;
        }
    }
}