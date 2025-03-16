using System;
using System.Collections.Generic;
using System.Linq;
using Slurs;
using UnityEngine;

namespace Fight
{
    public class Fight
    {
        public Opponent Opponent;
        public PlayerFighter PlayerFighter;
        public bool FightEnded { get; private set; }
        public FightTurn FightWinner { get; private set; }
        private FightTurn CurrentTurn { get; set; }

        public void StartFight(Opponent opponent, PlayerFighter playerFighter)
        {
            Opponent = opponent;
            PlayerFighter = playerFighter;

            CurrentTurn = FightTurn.Player;
        }

        public void Tick()
        {
            switch (CurrentTurn)
            {
                case FightTurn.Opponent:
                {
                    var slursList = Opponent.Fight();
                    Debug.Log($" opponent attack {String.Join(",", slursList.Select(x => x.SlurText).ToList())}");
                    var damage = CalculateDamage(slursList, PlayerFighter.GetWeakPoints());
                    Debug.Log($" opponent damage {damage}");
                    bool playerDied = PlayerFighter.TakeDamage(damage);
                    FightEnded |= playerDied;
                    Debug.Log($"player health {PlayerFighter.CurrentHealth}");
                    FightManager.Instance.ShowSlurs(slursList);
                    SwitchTurn();
                    break;
                }
                case FightTurn.Player when PlayerFighter.ReadyToFight:
                {
                    bool opponentDied =
                        Opponent.TakeDamage(CalculateDamage(PlayerFighter.Fight(), Opponent.GetWeakPoints()));
                    FightEnded |= opponentDied;
                    Debug.Log($"opponent health {Opponent.CurrentHealth}");
                    SwitchTurn();
                    break;
                }
            }

            if (FightEnded)
            {
                FightWinner = PlayerFighter.CurrentHealth > Opponent.CurrentHealth
                    ? FightTurn.Player
                    : FightTurn.Opponent;
                
                Debug.Log("Fight Ended");
            }
        }

        private void SwitchTurn()
        {
            CurrentTurn = CurrentTurn == FightTurn.Opponent ? FightTurn.Player : FightTurn.Opponent;
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