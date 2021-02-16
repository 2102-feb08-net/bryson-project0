using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    public class SearchMenu : Menu
    {
        MainDatabase _database;

        public SearchMenu(IIOController io, Menu previousMenu, MainDatabase database) : base(io, previousMenu)
        {
            _database = database;
        }

        public override void Open()
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption("Search by Customer", SearchByCustomer));
            response.Options.Add(new ChoiceOption("Search by Location", SearchByLocation));
            response.Options.Add(new ChoiceOption("Go Back", ReturnToPreviousMenu));
            response.ShowAndInvokeOptions();
        }

        private void SearchByCustomer()
        {
            _io.Output.Write("Enter their first name:");
            string firstName = _io.Input.ReadInput();

            _io.Output.Write("Enter their last name:");
            string lastName = _io.Input.ReadInput();

            List<Customer> customers = _database.CustomerDatabase.LookUpCustomer(firstName, lastName);

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

            var selectedCustomer = customers[0];
            var orders = _database.OrderHistory.SearchByCustomer(selectedCustomer);

            DisplayOrders(orders, selectedCustomer);
        }

        private void SearchByLocation()
        {

        }

        private void DisplayOrders(List<IOrder> orders, ICustomer customer)
        {
            if(orders.Count == 0)
            {
                _io.Output.Write($"The customer '{customer}' has no orders on record.");
                return;
            }

            OrderDisplayer displayer = new OrderDisplayer();
            foreach (string display in displayer.GetBatchOrderDisplay(orders))
                _io.Output.Write(display);
        }
    }
}
