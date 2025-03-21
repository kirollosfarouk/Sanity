﻿using System;
using System.Collections.Generic;
using System.Linq;
using Slurs;

namespace Fight
{
    public class Opponent : IFighter
    {
        public bool ReadyToFight { get; }
        
        
        public float StartingHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        private List<Slur> availableSlurs = new();
        private List<SlurCategory> weakPoints = new();

        public void Inialize(List<Slurs.Slur> intialSlurs, List<SlurCategory> oponentWeakPoints, float maxHealth)
        {
            availableSlurs.AddRange(intialSlurs);
            weakPoints.AddRange(oponentWeakPoints);
            StartingHealth = CurrentHealth = maxHealth;
        }

        public List<Slurs.Slur> Fight()
        {
            Random rand = new Random();
            return availableSlurs.OrderBy(x => rand.Next()).Take(3).ToList();
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