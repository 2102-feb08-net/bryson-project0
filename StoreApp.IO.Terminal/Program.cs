using System;
using System.Threading.Tasks;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    class Program
    {
        static IIOController io = new IOController(new Inputter(), new Outputter());

        static MainDatabase _mainDatabase = new MainDatabase();

        static async Task Main(string[] args)
        {
            await LoadDatabases();

            MainMenu mainMenu = new MainMenu(io, _mainDatabase);
            mainMenu.Open();
        }

        static async Task LoadDatabases()
        {
            io.Output.Write("Loading databases...");
            _mainDatabase.CustomerDatabase = await Serializer.DeserializeAsync<CustomerDatabase>(DatabasePaths.CUSTOMER_DATABASE_PATH);
            io.Output.Write("Finished loading databases...");
            io.Output.Write();
        }
    }
}
