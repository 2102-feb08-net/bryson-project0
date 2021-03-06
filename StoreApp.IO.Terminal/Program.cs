﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using StoreApp.DataAccess.Repository;
using StoreApp.Library;

namespace StoreApp.IO.Terminal
{
    internal class Program
    {
        private static readonly IIOController io = new IOController(new Inputter(), new Outputter());

        private static readonly MainDatabase _mainDatabase = new MainDatabase();

        private const string CONNECTION_STRING_PATH = @"R:\Revature\Projects\digitalStore-connection-string.txt";

        private const string CONNECTION_LOG_PATH = "connection_log.txt";
        private const string EXCEPTION_LOG_PATH = "exception_log.txt";

        private static async Task Main()
        {
            try
            {
                await LoadDatabases();
                MainMenu mainMenu = new MainMenu(io, _mainDatabase);
                await mainMenu.Open();
            }
            catch (Exception e)
            {
                LogFatalError(e);
            }
        }

        private static async Task LoadDatabases()
        {
            io.Output.Write("Loading databases...");
            string connectionString = await File.ReadAllTextAsync(CONNECTION_STRING_PATH);

            static void logger(string s)
            {
                Debug.WriteLine(s);
                using StreamWriter writer = new StreamWriter(CONNECTION_LOG_PATH, append: true);
                writer.WriteLine(s);
            }

            _mainDatabase.CustomerRepository = new CustomerRepository(connectionString, logger);
            _mainDatabase.LocationRepository = new LocationRepository(connectionString, logger);
            _mainDatabase.OrderRepository = new OrderRepository(connectionString, logger);
            _mainDatabase.ProductRepository = new ProductRepository(connectionString, logger);

            io.Output.Write("Finished loading databases...");
            io.Output.Write();
        }

        private static void LogFatalError(Exception e)
        {
            using StreamWriter writer = new StreamWriter(EXCEPTION_LOG_PATH, append: true);
            writer.WriteLine(DateTime.Now);
            writer.WriteLine(e.Message);
            writer.WriteLine(e.StackTrace);
            writer.Flush();

            Console.WriteLine($"FATAL ERROR: {e.Message}");
            Console.ReadLine();
        }
    }
}