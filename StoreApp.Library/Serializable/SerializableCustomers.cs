using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Library.Serializable
{
    public class SerializableCustomers
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }

    public static class SerialializableCustomersHelper
    {
        public static SerializableCustomers ExportToSerializable(this CustomerDatabase database)
        {
            SerializableCustomers serializable = new SerializableCustomers
            {
                Customers = new List<Customer>(database.Customers)
            };

            return serializable;
        }

        public static void ImportFromSerializable(this CustomerDatabase database, SerializableCustomers customers)
        {
        }
    }
}