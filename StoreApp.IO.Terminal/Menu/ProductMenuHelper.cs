using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.DataAccess.Repository;
using StoreApp.Library.Model;

namespace StoreApp.IO.Terminal
{
    public static class ProductMenuHelper
    {
        public static async Task<IProduct> LookUpProductAsync(IIOController io, MainDatabase database)
        {
            io.Output.Write("Enter the name of the product:");
            string productName = io.Input.ReadInput();

            ProductRepository repo = new ProductRepository(database.ConnectionString, database.Logger);

            IProduct product = await repo.LookupProductFromName(productName);

            if (product == null)
            {
                io.Output.Write($"A product with the name '{productName}' could not be found. The results are case-sensitive.");
            }

            return product;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static bool TryEnterProductQuantity(IIOController io, out int quantity)
        {
            io.Output.Write("Enter the quantity of the product:");
            string quantityString = io.Input.ReadInput();
            if (!int.TryParse(quantityString, out quantity))
            {
                io.Output.Write("Quantity must be an integer number");
                return false;
            }

            return true;
        }
    }
}