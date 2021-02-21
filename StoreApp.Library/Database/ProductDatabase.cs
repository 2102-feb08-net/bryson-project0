using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Library
{
    public class ProductDatabase
    {
        public List<ProductData> Products { get; set; } = new List<ProductData>();
    }
}