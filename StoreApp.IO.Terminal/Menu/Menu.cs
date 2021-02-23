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

        protected async Task TryAgain(string tryAgainText, string cancelText, Action tryAgain)
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption(tryAgainText, tryAgain));
            response.Options.Add(new ChoiceOption(cancelText));
            await response.ShowAndInvokeOptions();
        }
    }
}