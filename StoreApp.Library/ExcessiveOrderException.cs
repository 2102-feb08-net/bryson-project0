using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class ExcessiveOrderException : Exception
    {
        public ExcessiveOrderException() { }

        public ExcessiveOrderException(string message) : base(message) { }

        public ExcessiveOrderException(string message, Exception inner) : base(message, inner) { }
    }
}
