﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;

using System.Linq;

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

            var orders = _database.OrderHistory.SearchByCustomer(customer);
            DisplayOrders(orders, customer);
        }

        private void SearchOrdersByLocation()
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