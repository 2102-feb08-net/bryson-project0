using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library
{
    public record Product : IProduct
    {
        public string Name { get; init; }

        public string Category { get; init; }

        public Guid ID { get; init; }
    }
}
