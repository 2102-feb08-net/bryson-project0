using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DataAccess.Repository;
using StoreApp.Library;
using StoreApp.Library.Model;

namespace StoreApp.IO.Terminal
{
    public static class CustomerMenuHelper
    {
        public static async Task<Customer> LookUpCustomer(IIOController io, MainDatabase database)
        {
            io.Output.Write("Enter their first name:");
            string firstName = io.Input.ReadInput();

            io.Output.Write("Enter their last name:");
            string lastName = io.Input.ReadInput();

            CustomerRepository repo = new CustomerRepository(database.ConnectionString, database.Logger);

            List<Customer> customers = await repo.LookUpCustomersByNameAsync(firstName, lastName);

            if (customers.Count == 0)
            {
                io.Output.Write($"No customers found with the name {firstName} {lastName}.");
                return null;
            }

            if (customers.Count > 1)
            {
                io.Output.Write($"More than one customer was found with the name {firstName} {lastName}. Returning first one found.");
            }

            return customers[0];
        }

        public static async Task<List<ICustomer>> SearchCustomer(IIOController io, MainDatabase database)
        {
            io.Output.Write("Enter a search query for a name:");
            string searchQuery = io.Input.ReadInput();

            if (string.IsNullOrEmpty(searchQuery))
            {
                io.Output.Write("Search cannot be empty.");
                return null;
            }

            CustomerRepository repo = new CustomerRepository(database.ConnectionString, database.Logger);

            List<ICustomer> customers = await repo.SearchCustomersAsync(searchQuery);

            return customers;
        }
    }
}