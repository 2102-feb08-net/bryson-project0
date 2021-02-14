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


        IOutputter _outputter;
        IInputter _inputter;

        public ResponseChoice(IInputter inputter, IOutputter outputter)
        {
            _outputter = outputter;
            _inputter = inputter;
        }

        public void ShowAndInvokeOptions(params ChoiceOption[] options)
        {
            Options = new List<ChoiceOption>(options);
            DisplayOptions();
            InvokeChoice();
        }

        public void DisplayOptions()
        {
            _outputter.Write("Please select one of the following actions:");
            for(int i = 0; i < Options.Count; i++)
            {
                _outputter.Write($"{i + 1}. {Options[i].TextDescription}");
            }
        }

        public void InvokeChoice()
        {
            int? responseIndex = null;

            do
            {
                if (Options.Count < 1)
                    throw new IndexOutOfRangeException("Options must be at least 1");

                if (int.TryParse(_inputter.ReadInput(), out int responseValue))
                {
                    if (responseValue >= 1 && responseValue <= Options.Count)
                        responseIndex = responseValue - 1;
                }

                if (!responseIndex.HasValue)
                {
                    _outputter.Write("Input must be a valid option number.");
                    DisplayOptions();
                }

            } while (!responseIndex.HasValue);

            var action = Options[responseIndex.Value].ChoiceAction;
            action.Invoke();
            _outputter.Write("Press Enter to continue...");
            _inputter.ReadInput();
        }
    }
}
