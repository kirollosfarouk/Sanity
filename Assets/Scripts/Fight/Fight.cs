using System;
using System.Collections.Generic;
using System.Linq;
using Slurs;
using UI;
using UnityEngine;

namespace Fight
{
    public class Fight
    {
        public FightTurn currentTurn { get; private set; }
        private Opponent _opponent;
        private PlayerFighter _playerFighter;

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
                    Debug.Log($" opponent attack {slursList}");
                    var damage = CalculateDamage(slursList, _playerFighter.GetWeakPoints());
                    Debug.Log($" opponent damage {damage}");
                    _playerFighter.TakeDamage(damage);
                    Debug.Log($"player health {_playerFighter.CurrentHealth}");
                    SwitchTurn();
                    break;
                }
                case FightTurn.Player when _playerFighter.ReadyToFight:
                {
                    _opponent.TakeDamage(CalculateDamage(_playerFighter.Fight(), _opponent.GetWeakPoints()));
                    Debug.Log($"opponent health {_opponent.CurrentHealth}");
                    SwitchTurn();
                    break;
                }
                default:
                    Debug.Log("waiting for player to fight");
                    break;
            }
        }

        private void SwitchTurn()
        {
            currentTurn = currentTurn == FightTurn.Opponent ? FightTurn.Player : FightTurn.Opponent;
        }

        private float CalculateDamage(List<Slurs.Slurs> slurs, List<SlurCategory> weakPoints)
        {
            float damage = 0;

            foreach (Slurs.Slurs slur in slurs)
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