using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class ChoiceOption
    {
        public string TextDescription { get; }
        public readonly Action choiceAction;

        private readonly Func<Task> choiceTask;

        public ChoiceOption(string description)
        {
            TextDescription = description;
        }

        public ChoiceOption(string description, Action action)
        {
            TextDescription = description;
            choiceAction = action;
        }

        public ChoiceOption(string description, Func<Task> task)
        {
            TextDescription = description;
            choiceTask = task;
        }

        public async Task RunAsync()
        {
            if (choiceAction != null)
                await Task.Run(choiceAction);

            if (choiceTask != null)
                await choiceTask();
        }
    }
}