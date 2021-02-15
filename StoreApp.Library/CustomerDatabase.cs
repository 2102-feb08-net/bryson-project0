using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace StoreApp.Library
{
    public class CustomerDatabase
    {
        private List<Customer> _customers = new List<Customer>();

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public void AddCustomer(string firstName, string lastName)
        {
            Customer customer = new Customer() { FirstName = firstName, LastName = lastName, ID = Guid.NewGuid() };
            Customers.Add(customer);
        }

        public List<Customer> LookUpCustomer(string firstname, string lastName)
        {
            return Customers.FindAll(c => c.FirstName == firstname && c.LastName == lastName);
        }
    }
}