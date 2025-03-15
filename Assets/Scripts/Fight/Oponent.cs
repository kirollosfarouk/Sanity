using System;
using System.Collections.Generic;
using System.Linq;
using UI;

namespace Fight
{
    public class Oponent : IFighter
    {
        public bool ReadyToFight { get; }
        public float StartingHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        private List<Slurs> availableSlurs = new ();
        private List<SlurCategory> weakPoints = new ();

        public void Inialize(List<Slurs> intialSlurs, List<SlurCategory> oponentWeakPoints)
        {
            availableSlurs.AddRange(intialSlurs);
            weakPoints.AddRange(oponentWeakPoints);
        }

        public List<Slurs> Fight()
        {
            Random rand = new Random();
            return availableSlurs.OrderBy(x => rand.Next()).Take(3).ToList();
        }

        public bool TakeDamage(float damage)
        {
            return (CurrentHealth -= damage) <= 0;
        }

        public List<SlurCategory> GetWeakPoints()
        {
            return weakPoints;
        }
    }
}