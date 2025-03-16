using System;
using System.Collections.Generic;
using System.Linq;
using Slurs;
using UnityEngine;

namespace Fight
{
    public class Fight
    {
        public FightTurn currentTurn { get; private set; }
        private Opponent _opponent;
        public PlayerFighter _playerFighter;
        public bool FightEnded { get; private set; }
        public void StartFight(Opponent opponent,PlayerFighter playerFighter)
        {
            _opponent = opponent;
            _playerFighter = playerFighter;
            
            currentTurn = FightTurn.Player;
        }

        public void Tick()
        {
            switch (currentTurn)
            {
                case FightTurn.Opponent:
                {
                    var slursList = _opponent.Fight();
                    Debug.Log($" opponent attack {String.Join(",", slursList.Select(x => x.SlurText).ToList())}");
                    var damage = CalculateDamage(slursList, _playerFighter.GetWeakPoints());
                    Debug.Log($" opponent damage {damage}");
                    bool playerDied = _playerFighter.TakeDamage(damage);
                    FightEnded |= playerDied;
                    Debug.Log($"player health {_playerFighter.CurrentHealth}");
                    SwitchTurn();
                    break;
                }
                case FightTurn.Player when _playerFighter.ReadyToFight:
                {
                    bool opponentDied = _opponent.TakeDamage(CalculateDamage(_playerFighter.Fight(), _opponent.GetWeakPoints()));
                    FightEnded |= opponentDied;
                    Debug.Log($"opponent health {_opponent.CurrentHealth}");
                    SwitchTurn();
                    break;
                }
            }

            if (FightEnded)
            {
                Debug.Log("Fight Ended");
            }
        }

        private void SwitchTurn()
        {
            currentTurn = currentTurn == FightTurn.Opponent ? FightTurn.Player : FightTurn.Opponent;
        }

        private float CalculateDamage(List<Slur> slurs, List<SlurCategory> weakPoints)
        {
            float damage = 0;

            foreach (Slur slur in slurs)
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