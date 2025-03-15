using System.Collections.Generic;
using Slurs;
using UnityEngine;

namespace Fight
{
    public class FightManager : MonoBehaviour
    {
        private Fight _currentFight;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentFight = new Fight();
            
            Opponent opponent = new Opponent();
            opponent.Inialize(
                SlursGenerator.GetRandomSlurs(10), 
                new List<SlurCategory>() { SlurCategory.Physical },
                100);
            
            PlayerFighter playerFighter = new PlayerFighter();
            playerFighter.Inialize(
                SlursGenerator.UnlockedSlurs, 
                new List<SlurCategory>() { SlurCategory.Intellectual },
                100);

            _currentFight.StartFight(opponent, playerFighter);
        }

        // Update is called once per frame
        void Update()
        {
            _currentFight.Tick();
        }
    }
}
