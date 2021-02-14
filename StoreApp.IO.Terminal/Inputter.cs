using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class Inputter : IInputter
    {
        public string ReadInput() => Console.ReadLine();
    }
}
