using StoreApp.Library;
using StoreApp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;

namespace StoreApp.IO.Terminal
{
    public class MainMenu : Menu
    {
        private readonly MainDatabase _mainDatabase;

        public MainMenu(IIOController io, MainDatabase mainDatabase) : base(io, null)
        {
            _mainDatabase = mainDatabase;
        }

        public override async Task Open()
        {
            while (true)
            {
                _io.Output.Write("Welcome to the store!");

                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Place Order", PlaceOrder));
                response.Options.Add(new ChoiceOption("Search Database", Search));
                response.Options.Add(new ChoiceOption("Add Customer", AddCustomer));
                response.Options.Add(new ChoiceOption("Quit", Quit));
                await response.ShowAndInvokeOptions();
                _io.PressEnterToContinue();
            }
        }

        private async Task Search()
        {
            _io.Output.Write("Entering Search...");
            SearchMenu search = new SearchMenu(_io, this, _mainDatabase);
            await search.Open();
        }

        private async Task AddCustomer()
        {
            _io.Output.Write("Enter their first name:");
            string firstName = _io.Input.ReadInput();
            _io.Output.Write("Enter their last name:");
            string lastName = _io.Input.ReadInput();

            CustomerRepository repo = new CustomerRepository(_mainDatabase.ConnectionString, _mainDatabase.Logger);
            //_mainDatabase.CustomerDatabase.Customers.Add(new Customer(firstName, lastName));
            _io.Output.Write("Adding customer...");
            bool success = await repo.TryCreateCustomerAsync(firstName, lastName);
            //Serializer.SerializeAsync(_mainDatabase.CustomerDatabase, DatabasePaths.CUSTOMER_DATABASE_PATH).GetAwaiter().GetResult();

            if (success)
                _io.Output.Write($"'{firstName} {lastName}' has been added to the database.");
            else
                _io.Output.Write($"ERROR: Failed to write customer '{firstName} {lastName}' to the database");
        }

        private async Task PlaceOrder()
        {
            OrderMenu order = new OrderMenu(_io, this, _mainDatabase);
            await order.Open();
        }

        private void Quit() => Environment.Exit(0);
    }
}