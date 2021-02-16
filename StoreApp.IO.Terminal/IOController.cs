using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class IOController : IIOController
    {
        public IInputter Input { get; }
        public IOutputter Output { get; }


        public IOController(IInputter inputter, IOutputter outputter)
        {
            Input = inputter;
            Output = outputter;
        }

        public void PressEnterToContinue()
        {
            Output.Write("Press Enter to continue...");
            Input.ReadInput();
        }
    }
}
