using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slurs
{
    [Serializable]
    public class Slur
    {
        public string SlurText;

        public List<SlurCategory> Category;

        public Slur(string slurText, List<SlurCategory> category)
        {
            SlurText = slurText;
            Category = category;
        }
    }
}