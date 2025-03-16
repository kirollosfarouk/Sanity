using System.Collections.Generic;
using Slurs;
using UnityEngine;
using UnityEngine.Serialization;

namespace FighterData
{
    [CreateAssetMenu(fileName = "Fighter Data", menuName = "FighterData", order = 1)]
    public class FighterData : ScriptableObject
    {
        [FormerlySerializedAs("fighterPortrait")] public Sprite FighterPortrait;
        [FormerlySerializedAs("health")] public float Health;
        [FormerlySerializedAs("slurs")] public List<Slur> Slurs = new ();
        public List<SlurCategory> WeakPoints = new();
    }
}