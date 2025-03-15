using System.Collections.Generic;
using Slurs;
using UI;

namespace Fight
{
    public interface IFighter
    {
        public bool ReadyToFight { get; }
        public float StartingHealth { get; }
        public float CurrentHealth { get; }
        public void Inialize(List<Slurs.Slurs> intialSlurs, List<SlurCategory> oponentWeakPoints, float maxHealth);
        public List<Slurs.Slurs> Fight();
        public bool TakeDamage(float damage);
        public List<SlurCategory> GetWeakPoints();
    }
}