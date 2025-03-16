using Fight;
using UnityEngine;

public class BossBarricade : MonoBehaviour
{
    public int Threshold = 2;

    private int _enemiesDefeated;
    private bool _initialized;

    private void Update()
    {
        if (!_initialized && FightManager.Instance != null)
        {
            FightManager.Instance.BossBarricade = this;
            _initialized = true;
        }
    }

    public void EnemyDefeated()
    {
        _enemiesDefeated += 1;
        if (_enemiesDefeated >= Threshold)
            gameObject.SetActive(false);
    }
}