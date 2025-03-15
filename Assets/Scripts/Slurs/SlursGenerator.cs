using System;
using System.Collections.Generic;
using System.Linq;

namespace Slurs
{
    public static class SlursGenerator
    {
        public static List<Slur> AllSlurs = new ();
        public static List<Slur> UnlockedSlurs = new();

        public static void LoadSlurs()
        {
            AllSlurs.AddRange(new[]
                {
                    new Slur("weak", new List<SlurCategory> { SlurCategory.Physical }),
                    new Slur("baby", new List<SlurCategory> { SlurCategory.Physical, SlurCategory.Intellectual }),
                    new Slur("Stinken", new List<SlurCategory> { SlurCategory.Physical }),
                    new Slur("ugly", new List<SlurCategory> { SlurCategory.Visual }),
                    new Slur("scarecrow", new List<SlurCategory> { SlurCategory.Visual }),
                    new Slur("rolling", new List<SlurCategory> { SlurCategory.Visual }), 
                    new Slur("dumb", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slur("lying", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slur("Super", new List<SlurCategory> { SlurCategory.Intellectual }),
                    new Slur("Leg", new List<SlurCategory> { SlurCategory.Regular }),
                    new Slur("rolling", new List<SlurCategory> { SlurCategory.Regular }),
                }
            );

            UnlockedSlurs.AddRange(new[]
            {
                new Slur("weak", new List<SlurCategory> { SlurCategory.Physical }),
                new Slur("baby", new List<SlurCategory> { SlurCategory.Physical, SlurCategory.Intellectual }),
                new Slur("Stinken", new List<SlurCategory> { SlurCategory.Physical }),
                new Slur("Super", new List<SlurCategory> { SlurCategory.Regular }),
                new Slur("Leg", new List<SlurCategory> { SlurCategory.Regular }),
                new Slur("rolling", new List<SlurCategory> { SlurCategory.Regular })
            });
        }

        public static List<Slur> AddNewSlur(Slur slur)
        {
            UnlockedSlurs.Add(slur);
            return UnlockedSlurs;
        }

        public static List<Slur> GetRandomSlurs(int count)
        {
            Random random = new();
            return AllSlurs.OrderBy(x => random.Next()).Take(count).ToList();
        }
    }
}