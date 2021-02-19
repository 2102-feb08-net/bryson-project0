using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using StoreApp.Library.Serializable;

namespace StoreApp.Library
{
    public class CustomerDatabase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        /// <summary>
        /// Finds the customers in the database with the same first and last name.
        /// </summary>
        /// <param name="firstname">The first name of the customer</param>
        /// <param name="lastName">The last name of the customer</param>
        /// <returns>Returns a list of all of the customers found</returns>
        public List<Customer> LookUpCustomer(string firstname, string lastName)
        {
            return Customers.FindAll(c => c.FirstName == firstname && c.LastName == lastName);
        }
    }
}