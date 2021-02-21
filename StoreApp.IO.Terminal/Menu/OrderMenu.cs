﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;

namespace StoreApp.IO.Terminal
{
    public class OrderMenu : Menu
    {
        private IOrder _currentOrder;

        private readonly MainDatabase _database;

        public OrderMenu(IIOController io, Menu previousMenu, MainDatabase database) : base(io, previousMenu)
        {
            _database = database;
        }

        private bool buildingOrder = false;

        public override async Task Open()
        {
            buildingOrder = true;
            await CreateOrder();
        }

        private async Task CreateOrder()
        {
            Customer customer = await AttemptAddCustomerToNewOrder();
            if (customer == null)
                return;

            Location location = await AttemptAddLocationToOrder();

            if (location == null)
                return;

            _currentOrder = new Order(customer, location);

            while (buildingOrder)
            {
                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Add Product", AddNewProductToOrder));
                response.Options.Add(new ChoiceOption("Check Order Status", CheckStatus));
                response.Options.Add(new ChoiceOption("Cancel Order", CancelOrder));
                response.Options.Add(new ChoiceOption("Submit Order", TrySubmitOrder));
                await response.ShowAndInvokeOptions();
            }
        }

        private async Task<Customer> AttemptAddCustomerToNewOrder()
        {
            Customer customer = null;
            bool tryAgain;

            do
            {
                tryAgain = false;

                _io.Output.Write("Please select a customer to order as.");

                customer = await CustomerMenuHelper.LookUpCustomer(_io, _database);

                if (customer == null)
                    await TryAgain(() => tryAgain = true);
            } while (tryAgain);

            return customer;
        }

        private async Task<Location> AttemptAddLocationToOrder()
        {
            Location location = null;
            bool tryAgain;

            do
            {
                tryAgain = false;

                location = await LocationMenuHelper.LookUpLocation(_io, _database);

                if (location == null)
                    await TryAgain(() => tryAgain = true);
            } while (tryAgain);

            return location;
        }

        private async Task TryAgain(Action tryAgain)
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption("Try again with different values", tryAgain));
            response.Options.Add(new ChoiceOption("Cancel order"));
            await response.ShowAndInvokeOptions();
        }

        private void AddNewProductToOrder()
        {
            _io.Output.Write("Enter the name of the product you wish to add to the order:");
            string productName = _io.Input.ReadInput();
        }

        private void CancelOrder()
        {
            buildingOrder = false;
            _io.Output.Write("Order canceled.");
        }

        private void CheckStatus()
        {
            OrderDisplayer displayer = new OrderDisplayer();
            string display = displayer.GetOrderDisplay(_currentOrder);
            _io.Output.Write(display);
        }

        public async Task TrySubmitOrder()
        {
            _currentOrder.ProcessOrder();

            ProductRepository repo = new ProductRepository(_database.ConnectionString, _database.Logger);
            await repo.TryOrderTransaction(_currentOrder);

            buildingOrder = false;
        }
    }
}