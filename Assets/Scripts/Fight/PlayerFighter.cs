using System.Collections.Generic;
using Slurs;

namespace Fight
{
    public class PlayerFighter : IFighter
    {
        public bool ReadyToFight => selectedList.Count == 3;

        public float StartingHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        private List<Slur> availableSlurs = new();
        private List<SlurCategory> weakPoints = new();
        private List<Slur> selectedList = new();

        public void Inialize(List<Slur> intialSlurs, List<SlurCategory> oponentWeakPoints, float maxHealth)
        {
            availableSlurs.AddRange(intialSlurs);
            weakPoints.AddRange(oponentWeakPoints);
            StartingHealth = CurrentHealth = maxHealth;
        }

        public List<Slur> Fight()
        {
            var returnValue = new List<Slur>();
            returnValue.AddRange(selectedList);
            selectedList.Clear();
            
            return returnValue;
        }

        public void AddSlur(Slur slur)
        {
            selectedList.Add(slur);
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