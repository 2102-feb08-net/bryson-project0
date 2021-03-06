﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class ResponseChoice
    {
        /// <summary>
        /// The list of options for the user to choose from.
        /// </summary>
        public List<ChoiceOption> Options { get; private set; } = new List<ChoiceOption>();

        private ChoiceOption[] _currentOptions;
        private readonly IIOController _io;

        /// <summary>
        /// Creates a new ResponseChoice object
        /// </summary>
        /// <param name="io">The IO Controller used for input and output</param>
        /// <param name="waitForEnterKeyAfterResponse">Should the user be prompted to press enter again after the choice is finished executing?</param>
        public ResponseChoice(IIOController io)
        {
            _io = io;
        }

        /// <summary>
        /// Displays all of the options set in the Options list then prompts the user to select one of them.
        /// </summary>
        /// <param name="options">The options that the user can choose</param>
        public async Task ShowAndInvokeOptions(params ChoiceOption[] options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _currentOptions = new ChoiceOption[Options.Count];
            Options.CopyTo(_currentOptions);

            DisplayOptions();
            await InvokeChoice();
        }

        private void DisplayOptions()
        {
            _io.Output.Write("Please select one of the following actions:");
            for (int i = 0; i < _currentOptions.Length; i++)
            {
                _io.Output.Write($"{i + 1}. {_currentOptions[i].TextDescription}");
            }
        }

        private async Task InvokeChoice()
        {
            int? responseIndex = null;

            do
            {
                if (_currentOptions.Length < 1)
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

            await _currentOptions[responseIndex.Value].RunAsync();
        }
    }
}