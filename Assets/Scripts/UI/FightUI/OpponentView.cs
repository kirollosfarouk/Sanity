using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal class OpponentView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image opponentPortrait;
        
        public void Setup(Data.FighterData data)
        {
            healthSlider.value = 1;
            opponentPortrait.sprite = data.FighterPortrait;
        }
        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth/maxHealth;
        }
    }
}