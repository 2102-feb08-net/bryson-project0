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
        IInputter _inputter;
        IOutputter _outputter;

        OrderHistory _history;
        CustomerDatabase _database;

        public Searcher(IInputter inputter, IOutputter outputter, OrderHistory history, CustomerDatabase database)
        {
            _inputter = inputter;
            _outputter = outputter;
            _history = history;
            _database = database;
        }

        public void SearchByCustomer()
        {
            _outputter.Write("Enter their first name:");
            string firstName = _inputter.ReadInput();

            _outputter.Write("Enter their last name:");
            string lastName = _inputter.ReadInput();

            List<ICustomer> customers = _database.LookUpCustomer(firstName, lastName);

            if(customers.Count == 0)
            {
                _outputter.Write($"No customers found with the name {firstName} {lastName}.");
                return;
            }

            if(customers.Count > 1)
            {
                _outputter.Write($"More than one customer was found with the name {firstName} {lastName}");
                return;
            }

            _history.SearchByCustomer(customers[0]);
        }

        public void SearchByLocation()
        {

        }
    }
}
