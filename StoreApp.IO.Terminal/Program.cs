using System;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    class Program
    {
        static ResponseChoice response;

        static IOutputter outputter = new Outputter();
        static IInputter inputter = new Inputter();

        static CustomerDatabase database = new CustomerDatabase();

        static void Main(string[] args)
        {
            ChoiceOption search = new ("Search Database", Search);
            ChoiceOption addCustomer = new ("Add Customer", AddCustomer);
            ChoiceOption placeOrder = new("Place Order", PlaceOrder);
            ChoiceOption returnToMain = new("Return to main menu", MainMenu);
            ChoiceOption quit = new ChoiceOption("Quit", Quit);

            response = new ResponseChoice(inputter, outputter);

            MainMenu();
        }


        static void MainMenu()
        {
            ChoiceOption search = new("Search Database", Search);
            ChoiceOption addCustomer = new("Add Customer", AddCustomer);
            ChoiceOption placeOrder = new("Place Order", PlaceOrder);
            ChoiceOption quit = new ChoiceOption("Quit", Quit);

            outputter.Write("Welcome to the store!");

            response.ShowAndInvokeOptions(search, addCustomer, placeOrder, quit);
        }

        static void Search()
        {
            outputter.Write("Entering Search...");
            OrderHistory history = new OrderHistory();
            Searcher searcher = new Searcher(inputter, outputter, history, database);

            ChoiceOption searchByCustomer = new("Search by Customer", searcher.SearchByCustomer);
            ChoiceOption searchByLocation = new("Search by Location", searcher.SearchByLocation);
            ChoiceOption goBackToMainMenu = new("Go Back", MainMenu);

            response.ShowAndInvokeOptions(searchByCustomer, searchByLocation);
            MainMenu();
        }

        static void AddCustomer()
        {
            outputter.Write("Enter their first name:");
            string firstName = inputter.ReadInput();
            outputter.Write("Enter their last name:");
            string lastName = inputter.ReadInput();

            database.AddCustomer(firstName, lastName);
            outputter.Write($"'{firstName} {lastName}' has been added to the database.");
            MainMenu();
        }

        static void PlaceOrder()
        {
            MainMenu();
        }

        static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
