using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;

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
            response.Options.Add(new ChoiceOption("Search by Customer", SearchByCustomer));
            response.Options.Add(new ChoiceOption("Search by Location", SearchByLocation));
            response.Options.Add(new ChoiceOption("Go Back", ReturnToPreviousMenu));
            await response.ShowAndInvokeOptions();
        }

        private async Task SearchByCustomer()
        {
            ICustomer customer = await CustomerMenuHelper.LookUpCustomer(_io, _database);

            var orders = _database.OrderHistory.SearchByCustomer(customer);
            DisplayOrders(orders, customer);
        }

        private void SearchByLocation()
        {
            _io.Output.Write("Enter the name of the store location:");
            string name = _io.Input.ReadInput();
        }

        private void DisplayOrders(List<IOrder> orders, ICustomer customer)
        {
            if (orders.Count == 0)
            {
                _io.Output.Write($"The customer '{customer.DisplayName()}' has no orders on record.");
                return;
            }

            OrderDisplayer displayer = new OrderDisplayer();
            foreach (string display in displayer.GetBatchOrderDisplay(orders))
                _io.Output.Write(display);
        }
    }
}