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
        /// <summary>
        /// A helper method to ask for a name of a product and look it up in the database.
        /// </summary>
        /// <param name="io">IO controller to output and input text.</param>
        /// <param name="database">The main database.</param>
        /// <returns>Returns a product as a result of the look up. May be null if not found.</returns>
        public static async Task<IProduct> LookUpProductAsync(IIOController io, MainDatabase database)
        {
            io.Output.Write("Enter the name of the product:");
            string productName = io.Input.ReadInput();

            IProductRepository repo = database.ProductRepository;

            IProduct product = await repo.LookupProductFromName(productName);

            if (product is null)
            {
                io.Output.Write($"A product with the name '{productName}' could not be found. The results are case-sensitive.");
            }

            return product;
        }

        /// <summary>
        /// A helper method to ask for the quantity of a product and then prompt for user input to be converted to an integer..
        /// </summary>
        /// <remarks>Does not do any validation on the integer value (i.e less than 0)</remarks>
        /// <param name="io">IO controller to output and input text.</param>
        /// <param name="quantity">The quantity converted from the users input.</param>
        /// <returns>Returns whether it was successful in converting the user input into an int.</returns>
        public static bool TryEnterProductQuantity(IIOController io, out int quantity)
        {
            io.Output.Write("Enter the quantity of the product:");
            string quantityString = io.Input.ReadInput();
            if (!int.TryParse(quantityString, out quantity))
            {
                io.Output.Write("Quantity must be an integer number.");
                return false;
            }

            return true;
        }
    }
}