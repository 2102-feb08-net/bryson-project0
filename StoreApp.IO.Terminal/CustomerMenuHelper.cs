using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public static class CustomerMenuHelper
    {
        public static Customer LookUpCustomer(IIOController io, CustomerDatabase database)
        {
            io.Output.Write("Enter their first name:");
            string firstName = io.Input.ReadInput();

            io.Output.Write("Enter their last name:");
            string lastName = io.Input.ReadInput();

            List<Customer> customers = database.LookUpCustomer(firstName, lastName);

            if (customers.Count == 0)
            {
                io.Output.Write($"No customers found with the name {firstName} {lastName}.");
                return null;
            }

            if (customers.Count > 1)
            {
                io.Output.Write($"More than one customer was found with the name {firstName} {lastName}. Which customer GUID do you want?");
                throw new NotImplementedException();
            }

            return customers[0];
        }
    }
}
