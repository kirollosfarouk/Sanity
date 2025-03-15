using System.Collections.Generic;
using UI;

namespace Fight
{
    public interface IFighter
    {
        public bool ReadyToFight { get; }
        public float StartingHealth { get; }
        public float CurrentHealth { get; }
        public void Inialize(List<Slurs> intialSlurs, List<SlurCategory> oponentWeakPoints);
        public List<Slurs> Fight();
        public bool TakeDamage(float damage);
        public List<SlurCategory> GetWeakPoints();
    }
}