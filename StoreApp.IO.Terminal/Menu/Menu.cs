using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public abstract class Menu
    {
        protected Menu _previousMenu;
        protected IIOController _io;

        public Menu(IIOController io, Menu previousMenu)
        {
            _previousMenu = previousMenu;
            _io = io;
        }

        public abstract Task Open();

        protected void ReturnToPreviousMenu()
        {
            if (_previousMenu is not null)
                _previousMenu.Open();
            else
                Environment.Exit(0);
        }
    }
}