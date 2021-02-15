using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public interface IIOController
    {
        IInputter Input { get; }
        IOutputter Output { get; }

        void PressEnterToContinue();
    }
}
