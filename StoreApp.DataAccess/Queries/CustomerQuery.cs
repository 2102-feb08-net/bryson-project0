using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Queries
{
    public static class CustomerQuery
    {
        public static IQueryable<Customer> GetCustomersFromName(DigitalStoreContext context, string firstName, string lastName)
        {
            string trimmedFirst = firstName.Trim();
            string trimmedLast = string.IsNullOrWhiteSpace(lastName) ? null : lastName.Trim();

            var customerQuery = context.Customers.Where(c => c.FirstName == trimmedFirst && c.LastName == trimmedLast);
            return customerQuery;
        }
    }
}