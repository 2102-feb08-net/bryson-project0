using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class Outputter : IOutputter
    {
        public void Write(string outputText) => Console.WriteLine(outputText);
        public void Write() => Console.WriteLine();
    }
}
