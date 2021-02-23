using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public record Product : IProduct
    {
        public string Name { get; }

        public string Category { get; }

        public decimal UnitPrice { get; }

        public int Id { get; }

        /// <summary>
        /// Constructs a new ProductData
        /// </summary>
        /// <param name="name">The name of the product. Cannot be null or empty.</param>
        /// <param name="category">The category of the product. Cannot be null or empty.</param>
        /// <param name="unitPrice">The price in USD of the product. Must be greater than or equal to 0.</param>
        /// <param name="id">The product ID of the product. Must be greater than 0.</param>
        public Product(string name, string category, decimal unitPrice, int id)
        {
            if (name is null)
                throw new ArgumentNullException(paramName: nameof(name), message: "Product name cannot be null.");

            if (category is null)
                throw new ArgumentNullException(paramName: nameof(category), message: "Product category cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(paramName: nameof(name), message: "Product name cannot be empty or whitespace.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(paramName: nameof(category), message: "Product category cannot be empty or whitespace.");

            if (unitPrice < 0)
                throw new ArgumentException(paramName: nameof(unitPrice), message: "The price cannot be negative.");

            if (id <= 0)
                throw new ArgumentException(paramName: nameof(id), message: "The Product ID must be greater than 0.");

            Name = name ?? throw new NullReferenceException();

            Category = category ?? throw new NullReferenceException();

            if (unitPrice < 0)
                throw new ArgumentException("Price cannot be below 0.");
            UnitPrice = unitPrice;

            Id = id;
        }
    }
}