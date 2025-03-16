using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class OpponentView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image opponentPortrait;

        public void Setup(FighterData.FighterData data)
        {
            healthSlider.value = 1;
            opponentPortrait.sprite = data.fighterPortrait;
        }
        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth/maxHealth;
        }
    }
}