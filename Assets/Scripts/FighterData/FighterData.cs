using System.Collections.Generic;
using Slurs;
using UnityEngine;

namespace FighterData
{
    [CreateAssetMenu(fileName = "Fighter Data", menuName = "FighterData", order = 1)]
    public class FighterData : ScriptableObject
    {
        public Sprite fighterPortrait;
        public float health;
        public List<Slur> slurs = new ();
    }
}