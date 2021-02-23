using StoreApp.Library;
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
            ICustomerRepository repo = _mainDatabase.CustomerRepository;

            INewCustomer customer = await MakeNewCustomer();

            if (customer == null)
                return;

            _io.Output.Write("Adding customer...");
            await repo.CreateCustomerAsync(customer);
            _io.Output.Write($"'{customer}' has been added to the database.");
        }

        private async Task<INewCustomer> MakeNewCustomer()
        {
            INewCustomer newCustomer = null;
            string firstName, lastName;
            bool tryAgain;
            do
            {
                tryAgain = false;

                _io.Output.Write("Enter their first name:");
                firstName = _io.Input.ReadInput();
                _io.Output.Write("Enter their last name:");
                lastName = _io.Input.ReadInput();

                if (string.IsNullOrWhiteSpace(firstName))
                {
                    _io.Output.Write("The first name cannot be empty.");
                    await TryAgain("Try again with a new customer name", "Cancel adding customer", () => tryAgain = true);
                }
                else
                    newCustomer = new NewCustomer(firstName, lastName);
            }
            while (tryAgain);

            return newCustomer;
        }

        private async Task PlaceOrder()
        {
            OrderMenu order = new OrderMenu(_io, this, _mainDatabase);
            await order.Open();
        }

        private void Quit() => Environment.Exit(0);
    }
}