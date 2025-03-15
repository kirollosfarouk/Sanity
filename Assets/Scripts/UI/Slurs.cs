﻿using System.Collections.Generic;

namespace UI
{
    public class Slurs
    {
        public string SlurText { get; }
        public List<SlurCategory> Category { get; }

        public Slurs(string slurText, List<SlurCategory> category)
        {
            SlurText = slurText;
            Category = category;
        }
    }
}