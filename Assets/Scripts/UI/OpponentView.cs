using Fight;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UI
{
    internal class OpponentView : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image opponentPortrait;

        public void Setup(FightManager fightManager)
        {
            
        }
        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth/maxHealth;
        }
    }
}