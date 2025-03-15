using System.Collections.Generic;

namespace Slurs
{
    public class Slur
    {
        public string SlurText { get; }
        public List<SlurCategory> Category { get; }

        public Slur(string slurText, List<SlurCategory> category)
        {
            SlurText = slurText;
            Category = category;
        }
    }
}