using System;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    class Program
    {
        static ResponseChoice response;

        static IIOController io = new IOController(new Inputter(), new Outputter());

        static Serializer serializer = new Serializer();

        static CustomerDatabase database = new CustomerDatabase();

        const string CUSTOMER_DATABASE_PATH = "customers.json";


        static async Task Main(string[] args)
        {
            await LoadDatabases();

            response = new ResponseChoice(io);

            MainMenu();
        }

        static async Task LoadDatabases()
        {
            io.Output.Write("Loading databases...");
            database = await serializer.DeserializeAsync<CustomerDatabase>(CUSTOMER_DATABASE_PATH);
            io.Output.Write("Finished loading databases...");
            io.Output.Write();
        }

        static void MainMenu()
        {
            ChoiceOption placeOrder = new("Place Order", PlaceOrder);
            ChoiceOption search = new("Search Database", Search);
            ChoiceOption addCustomer = new("Add Customer", AddCustomer);
            ChoiceOption quit = new ChoiceOption("Quit", Quit);

            io.Output.Write("Welcome to the store!");

            response.ShowAndInvokeOptions(placeOrder, search, addCustomer, quit);
        }

        static void Search()
        {
            io.Output.Write("Entering Search...");
            OrderHistory history = new OrderHistory();
            Searcher searcher = new Searcher(io, history, database);

            ChoiceOption searchByCustomer = new("Search by Customer", searcher.SearchByCustomer);
            ChoiceOption searchByLocation = new("Search by Location", searcher.SearchByLocation);
            ChoiceOption goBackToMainMenu = new("Go Back", MainMenu);

            response.ShowAndInvokeOptions(searchByCustomer, searchByLocation);
            MainMenu();
        }

        static void AddCustomer()
        {
            io.Output.Write("Enter their first name:");
            string firstName = io.Input.ReadInput();
            io.Output.Write("Enter their last name:");
            string lastName = io.Input.ReadInput();

            database.AddCustomer(firstName, lastName);
            io.Output.Write("Adding customer...");
            serializer.SerializeAsync(database, CUSTOMER_DATABASE_PATH).GetAwaiter().GetResult();
            io.Output.Write($"'{firstName} {lastName}' has been added to the database.");
            io.PressEnterToContinue();

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
