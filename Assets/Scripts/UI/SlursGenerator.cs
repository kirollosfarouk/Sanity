using System.Collections.Generic;

namespace UI
{
    public static class SlursGenerator
    {
        public static List<Slurs> AllSlurs = new ();
        public static List<Slurs> UnlockedSlurs = new();

        public static void LoadSlurs()
        {
            AllSlurs.AddRange(new[]
                {
                    new Slurs("weak", new List<SlurCategory> { SlurCategory.Physical }),
                    new Slurs("baby", new List<SlurCategory> { SlurCategory.Physical, SlurCategory.Intellectual }),
                    new Slurs("Stinken", new List<SlurCategory> { SlurCategory.Physical }),
                    new Slurs("ugly", new List<SlurCategory> { SlurCategory.Visual }),
                    new Slurs("scarecrow", new List<SlurCategory> { SlurCategory.Visual }),
                    new Slurs("rolling", new List<SlurCategory> { SlurCategory.Visual }), 
                    new Slurs("dumb", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slurs("lying", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slurs("Super", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slurs("Leg", new List<SlurCategory> { SlurCategory.Regular }),
                    new Slurs("rolling", new List<SlurCategory> { SlurCategory.Regular }),
                }
            );

            UnlockedSlurs.AddRange(new[]
            {
                new Slurs("weak", new List<SlurCategory> { SlurCategory.Physical }),
                new Slurs("baby", new List<SlurCategory> { SlurCategory.Physical, SlurCategory.Intellectual }),
                new Slurs("Stinken", new List<SlurCategory> { SlurCategory.Physical }),
                new Slurs("Super", new List<SlurCategory> { SlurCategory.Regular }),
                new Slurs("Leg", new List<SlurCategory> { SlurCategory.Regular }),
                new Slurs("rolling", new List<SlurCategory> { SlurCategory.Regular })
            });
        }

        public static List<Slurs> AddNewSlur(Slurs slurs)
        {
            UnlockedSlurs.Add(slurs);
            return UnlockedSlurs;
        }
    }
}