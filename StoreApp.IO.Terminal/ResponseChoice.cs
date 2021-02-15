using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class ResponseChoice
    {
        public List<ChoiceOption> Options { get; private set; }


        IIOController _io;

        public ResponseChoice(IIOController io)
        {
            _io = io;
        }

        public void ShowAndInvokeOptions(params ChoiceOption[] options)
        {
            Options = new List<ChoiceOption>(options);
            DisplayOptions();
            InvokeChoice();
        }

        public void DisplayOptions()
        {
            _io.Output.Write("Please select one of the following actions:");
            for(int i = 0; i < Options.Count; i++)
            {
                _io.Output.Write($"{i + 1}. {Options[i].TextDescription}");
            }
        }

        public void InvokeChoice()
        {
            int? responseIndex = null;

            do
            {
                if (Options.Count < 1)
                    throw new IndexOutOfRangeException("Options must be at least 1");

                if (int.TryParse(_io.Input.ReadInput(), out int responseValue))
                {
                    if (responseValue >= 1 && responseValue <= Options.Count)
                        responseIndex = responseValue - 1;
                }

                if (!responseIndex.HasValue)
                {
                    _io.Output.Write("Input must be a valid option number.");
                    DisplayOptions();
                }

            } while (!responseIndex.HasValue);

            var action = Options[responseIndex.Value].ChoiceAction;
            action.Invoke();
            _io.PressEnterToContinue();
        }
    }
}
