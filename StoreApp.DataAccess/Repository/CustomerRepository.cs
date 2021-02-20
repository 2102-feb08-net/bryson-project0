using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(string connectionString, Action<string> logger) : base(connectionString, logger)
        {
        }

        public async Task<List<Library.Model.Customer>> LookUpCustomersByNameAsync(string firstName, string lastName)
        {
            using var context = new DigitalStoreContext(Options);

            var customers = await context.Customers.Where(c => c.FirstName == firstName && c.LastName == lastName).ToListAsync();
            return customers.Select(c => new Library.Model.Customer(c.FirstName, c.LastName, c.Id)).ToList();
        }

        public async Task<bool> TryCreateCustomerAsync(string firstName, string lastName)
        {
            using var context = new DigitalStoreContext(Options);

            int id = await GenerateNextIdAsync(context.Customers);

            await context.Customers.AddAsync(new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                Id = id
            }
            );

            return await context.SaveChangesAsync() > 0;
        }
    }
}