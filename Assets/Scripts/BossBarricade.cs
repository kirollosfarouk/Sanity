using Fight;
using UnityEngine;

public class BossBarricade : MonoBehaviour
{
    private int _enemiesDefeated;
    public int Threshold = 2;

    private void Start()
    {
        FightManager.Instance.BossBarricade = this;
    }

    public void EnemyDefeated()
    {
        _enemiesDefeated += 1;
        if (_enemiesDefeated >= Threshold)
            gameObject.SetActive(false);
    }
}