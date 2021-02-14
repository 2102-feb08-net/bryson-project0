using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class CustomerDatabase
    {
        private List<ICustomer> _customers = new List<ICustomer>();
        public List<ICustomer> Customers = new List<ICustomer>();

        public void AddCustomer(string firstName, string lastName)
        {
            ICustomer customer = new Customer(firstName, lastName, Guid.NewGuid());
            _customers.Add(customer);
        }

        public List<ICustomer> LookUpCustomer(string firstname, string lastName)
        {
            return _customers.FindAll(c => c.FirstName == firstname && c.LastName == lastName);
        }
    }
}
