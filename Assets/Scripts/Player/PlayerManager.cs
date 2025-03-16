using System;
using System.Collections.Generic;
using Data;
using Slurs;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [SerializeField] private FighterData data;
        
        public List<Slur> UnlockedSlurs = new();

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

        private void Start()
        {
            UnlockedSlurs.AddRange(data.Slurs);
        }

        public void UnlockSlur(Slur slur)
        {
            UnlockedSlurs.Add(slur);
        }

        public void UnlockSlur(List<Slur> slur)
        {
            UnlockedSlurs.AddRange(slur);
        }
    }
}