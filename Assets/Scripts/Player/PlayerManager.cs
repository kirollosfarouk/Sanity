using System.Collections.Generic;
using System.Linq;
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
        private List<Slur> allSlurs = new();
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
            allSlurs.AddRange(data.Slurs);
            UnlockedSlurs.AddRange(GetRandomSlurs(9));
        }

        private List<Slur> GetRandomSlurs(int count)
        {
            System.Random random = new();
            return allSlurs.OrderBy(x => random.Next()).Take(count).ToList();
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