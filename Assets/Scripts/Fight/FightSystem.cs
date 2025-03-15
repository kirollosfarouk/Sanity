using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Fight
{
    public class FightSystem
    {
        public FightTurn currentTurn { get; private set; }
        private Oponent _oponent;

        public void StartFight(Oponent oponent)
        {
            _oponent = oponent;
            currentTurn = FightTurn.Player;
        }

        private void Tick()
        {
            if (currentTurn == FightTurn.Opponent)
            {
                List<Slurs> slursList = _oponent.Fight();
            }
        }

        private float CalculateDamage(List<Slurs> slurs, List<SlurCategory> weakPoints)
        {
            float damage = 0;
            
            foreach (Slurs slur in slurs)
            {
                damage += 5;
                
                if (slur.Category.Intersect(weakPoints).Any())
                {
                    damage *= 2;
                }
            }
            
            return damage;
        }
    }

    public enum FightTurn
    {
        Player,
        Opponent
    }
}