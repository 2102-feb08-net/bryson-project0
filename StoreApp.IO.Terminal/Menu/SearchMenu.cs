using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;

using System.Linq;

using StoreApp.DataAccess.Repository;

namespace StoreApp.IO.Terminal
{
    public class SearchMenu : Menu
    {
        private readonly MainDatabase _database;

        public SearchMenu(IIOController io, Menu previousMenu, MainDatabase database) : base(io, previousMenu)
        {
            _database = database;
        }

        public override async Task Open()
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption("Search List of Customers", SearchListOfCustomers));
            response.Options.Add(new ChoiceOption("Search Orders by Customer", SearchOrdersByCustomer));
            response.Options.Add(new ChoiceOption("Search Orders by Store Location", SearchOrdersByLocation));
            response.Options.Add(new ChoiceOption("Go Back", ReturnToPreviousMenu));
            await response.ShowAndInvokeOptions();
        }

        private async Task SearchListOfCustomers()
        {
            List<ICustomer> foundCustomers = await CustomerMenuHelper.SearchCustomer(_io, _database);

            if (foundCustomers.Count > 0)
            {
                _io.Output.Write("Customers found:");

                foundCustomers.ForEach(c => _io.Output.Write(c.DisplayName()));
            }
            else
            {
                _io.Output.Write("No customers found.");
            }
        }

        private async Task SearchOrdersByCustomer()
        {
            ICustomer customer = await CustomerMenuHelper.LookUpCustomer(_io, _database);

            OrderRepository repo = new OrderRepository(_database.ConnectionString, _database.Logger);
            var orders = await repo.GetOrdersFromCustomer(customer);

            if (orders.Count == 0)
            {
                _io.Output.Write($"The customer '{customer.DisplayName()}' has no orders on record.");
                return;
            }

            await DisplayOrdersWithSortOptions(orders);
        }

        private async Task SearchOrdersByLocation()
        {
            _io.Output.Write("Enter the name of the store location:");
            string name = _io.Input.ReadInput();

            OrderRepository repo = new OrderRepository(_database.ConnectionString, _database.Logger);
            var orders = await repo.GetOrdersFromLocation(name);

            if (orders.Count == 0)
            {
                _io.Output.Write($"No orders were found on record for '{name}'. Either no records have ever occured or the location does not exist.");
                return;
            }
            await DisplayOrdersWithSortOptions(orders);
        }

        private async Task DisplayOrdersWithSortOptions(IEnumerable<IReadOnlyOrder> orders)
        {
            bool isSearching = true;
            do
            {
                DisplayOrders(orders);

                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Sort by earliest order time", () => orders = orders.SortByEarliest()));
                response.Options.Add(new ChoiceOption("Sort by latest order time", () => orders = orders.SortByLatest()));
                response.Options.Add(new ChoiceOption("Sort by cheapest total price", () => orders = orders.SortByCheapest()));
                response.Options.Add(new ChoiceOption("Sort by most expensive total price", () => orders = orders.SortByMostExpensive()));
                response.Options.Add(new ChoiceOption("Stop searching", () => isSearching = false));
                await response.ShowAndInvokeOptions();
            } while (isSearching);
        }

        private void DisplayOrders(IEnumerable<IReadOnlyOrder> orders)
        {
            OrderDisplayer displayer = new OrderDisplayer();
            foreach (string display in displayer.GetBatchOrderDisplay(orders))
                _io.Output.Write(display);
        }
    }
}