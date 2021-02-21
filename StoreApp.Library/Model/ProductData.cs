using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class ProductData : IProduct
    {
        public string Name { get; }

        public string Category { get; }

        public ProductData(string name, string category)
        {
            Name = name ?? throw new NullReferenceException();
            Category = category ?? throw new NullReferenceException();
        }
    }
}