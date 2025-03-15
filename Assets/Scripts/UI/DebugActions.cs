using System.Collections.Generic;
using Slurs;
using UnityEngine;

public class DebugActions : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
            RewardSelectionView.Instance.Show(new List<Slur> { new("Test1", new List<SlurCategory>()), new("Test2", new List<SlurCategory>()), new("Test3", new List<SlurCategory>()) });
    }
}