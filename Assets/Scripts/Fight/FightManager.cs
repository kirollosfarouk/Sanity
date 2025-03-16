using System.Collections.Generic;
using Slurs;
using UI;
using UnityEngine;
namespace Fight
{
    public class FightManager : MonoBehaviour
    {
        public static FightManager Instance { get; private set; }
        
        [SerializeField] private PlayerView PlayerView;
        [SerializeField] private OpponentView OpponentView;
        
        private Fight _currentFight;
        void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
        
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

        public void NewFight(Opponent opponent, PlayerFighter playerFighter)
        {
            _currentFight = new Fight();
            _currentFight.StartFight(opponent, playerFighter);
        }

        void Update()
        {
            if (!_currentFight.FightEnded)
            {
                _currentFight.Tick();
                PlayerView.UpdateHealth(_currentFight._playerFighter.CurrentHealth,
                    _currentFight._playerFighter.StartingHealth);
            }
        }

        public void WordSelected(Slur word)
        {
            _currentFight._playerFighter.AddSlur(word);
        }
    }
}
