using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A customer who can order make an order and has been added to the database.
    /// </summary>
    public interface ICustomer : INewCustomer
    {
        /// <summary>
        /// The Id of the customer
        /// </summary>
        int Id { get; }
    }
}