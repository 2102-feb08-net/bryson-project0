using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public class Searcher
    {
        OrderHistory _history;
        CustomerDatabase _database;

        IIOController _io;

        public Searcher(IIOController io, OrderHistory history, CustomerDatabase database)
        {
            _io = io;
            _history = history;
            _database = database;
        }

        public void SearchByCustomer()
        {
            _io.Output.Write("Enter their first name:");
            string firstName = _io.Input.ReadInput();

            _io.Output.Write("Enter their last name:");
            string lastName = _io.Input.ReadInput();

            List<Customer> customers = _database.LookUpCustomer(firstName, lastName);

            if(customers.Count == 0)
            {
                _io.Output.Write($"No customers found with the name {firstName} {lastName}.");
                return;
            }

            if(customers.Count > 1)
            {
                _io.Output.Write($"More than one customer was found with the name {firstName} {lastName}");
                return;
            }

            _history.SearchByCustomer(customers[0]);
        }

        public void SearchByLocation()
        {

        }
    }
}
