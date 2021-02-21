using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class MainDatabase
    {
        public string ConnectionString { get; set; }

        public Action<string> Logger { get; set; }
    }
}