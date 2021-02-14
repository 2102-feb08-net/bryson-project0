﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.IO.Terminal
{
    public class ChoiceOption
    {
        public string TextDescription { get; }
        public Action ChoiceAction { get; }

        public ChoiceOption(string description, Action action)
        {
            TextDescription = description;
            ChoiceAction = action ?? throw new NullReferenceException("Every choice must have an action.");
        }
    }
}
