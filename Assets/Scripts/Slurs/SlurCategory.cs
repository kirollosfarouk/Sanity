using System.Collections.Generic;
using UnityEngine;

namespace Slurs
{
    public enum SlurCategory
    {
        Physical,
        Visual,
        Intellectual,
        Regular
    }

    public static class CategoryColor
    {
        public static Dictionary<SlurCategory, Color> SlurColors = new()
        {
            { SlurCategory.Physical ,Color.red},
            { SlurCategory.Visual ,Color.green},
            { SlurCategory.Intellectual ,Color.blue},
            { SlurCategory.Regular ,Color.gray},
        };
    }
}