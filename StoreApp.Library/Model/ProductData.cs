using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public record ProductData : IProduct
    {
        public string Name { get; }

        public string Category { get; }

        public decimal UnitPrice { get; }

        /// <summary>
        /// Constructs a new ProductData
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="category">The category of the product.</param>
        /// <param name="unitPrice">The price in USD of the product.</param>
        public ProductData(string name, string category, decimal unitPrice)
        {
            Name = name ?? throw new NullReferenceException();
            Category = category ?? throw new NullReferenceException();

            if (unitPrice < 0)
                throw new ArgumentException("Price cannot be below 0.");
            UnitPrice = unitPrice;
        }
    }
}