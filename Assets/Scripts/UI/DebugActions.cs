using System.Collections.Generic;
using Fight;
using Slurs;
using UnityEngine;

public class DebugActions : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            RewardSelectionView.Instance.Show(new List<Slur> { new("Test1", new List<SlurCategory>()), new("Test2", new List<SlurCategory>()), new("Test3", new List<SlurCategory>()) });
        if (Input.GetKeyDown(KeyCode.F3))
            MenuStateHandler.Instance.Won();
        if (Input.GetKeyDown(KeyCode.F4))
            MenuStateHandler.Instance.Lost();
        if (Input.GetKeyDown(KeyCode.F5))
            FightManager.Instance?.StartDebugFight();
    }
}