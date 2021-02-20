using System;
using System.IO;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Serialization;

namespace StoreApp.IO.Terminal
{
    internal class Program
    {
        private static readonly IIOController io = new IOController(new Inputter(), new Outputter());

        private static readonly MainDatabase _mainDatabase = new MainDatabase();

        private const string CONNECTION_STRING_PATH = @"E:\Revature\Projects\digitalStore-connection-string.txt";

        private static async Task Main()
        {
            await LoadDatabases();

            MainMenu mainMenu = new MainMenu(io, _mainDatabase);
            mainMenu.Open();
        }

        private static async Task LoadDatabases()
        {
            io.Output.Write("Loading databases...");
            _mainDatabase.CustomerDatabase = await Serializer.DeserializeAsync<CustomerDatabase>(DatabasePaths.CUSTOMER_DATABASE_PATH);
            _mainDatabase.ConnectionString = await File.ReadAllTextAsync(CONNECTION_STRING_PATH);
            io.Output.Write("Finished loading databases...");
            io.Output.Write();
        }
    }
}