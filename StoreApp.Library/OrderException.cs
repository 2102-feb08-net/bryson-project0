using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// An exception for when an order is unable to be processed.
    /// </summary>
    public class OrderException : Exception
    {
        /// <summary>
        /// Constructs and OrderException with the specified message,
        /// </summary>
        /// <param name="message">The message to print with the exception.</param>
        public OrderException(string message) : base(message)
        {
        }
    }
}