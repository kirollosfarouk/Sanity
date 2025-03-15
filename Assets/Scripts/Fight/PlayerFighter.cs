using System.Collections.Generic;
using UI;

namespace Fight
{
    public class PlayerFighter : IFighter
    {
        public bool ReadyToFight { get; }
        public float StartingHealth { get; }
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
            throw new System.NotImplementedException();
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