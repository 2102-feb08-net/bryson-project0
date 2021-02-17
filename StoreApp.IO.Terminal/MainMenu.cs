using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class MainMenu : Menu
    {
        MainDatabase _mainDatabase;

        public MainMenu(IIOController io, MainDatabase mainDatabase) : base(io, null) 
        {
            _mainDatabase = mainDatabase;
        }

        public override void Open()
        {
            while (true)
            {
                _io.Output.Write("Welcome to the store!");

                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Place Order", PlaceOrder));
                response.Options.Add(new ChoiceOption("Search Database", Search));
                response.Options.Add(new ChoiceOption("Add Customer", AddCustomer));
                response.Options.Add(new ChoiceOption("Quit", Quit));
                response.ShowAndInvokeOptions();
                _io.PressEnterToContinue();
            }
        }

        void Search()
        {
            _io.Output.Write("Entering Search...");
            SearchMenu search = new SearchMenu(_io, this, _mainDatabase);
            search.Open();
        }

        void AddCustomer()
        {
            _io.Output.Write("Enter their first name:");
            string firstName = _io.Input.ReadInput();
            _io.Output.Write("Enter their last name:");
            string lastName = _io.Input.ReadInput();

            _mainDatabase.CustomerDatabase.AddCustomer(firstName, lastName);
            _io.Output.Write("Adding customer...");
            Serializer.SerializeAsync(_mainDatabase.CustomerDatabase, DatabasePaths.CUSTOMER_DATABASE_PATH).GetAwaiter().GetResult();
            _io.Output.Write($"'{firstName} {lastName}' has been added to the database.");
        }

        void PlaceOrder()
        {
            OrderMenu order = new OrderMenu(_io, this, _mainDatabase.CustomerDatabase);
            order.Open();
        }

        void Quit()
        {
            Environment.Exit(0);
        }
    }
}
