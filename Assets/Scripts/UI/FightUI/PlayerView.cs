using System.Collections.Generic;
using Slurs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject Content;
        [SerializeField] private SlurButton slurGameObject;
        [SerializeField] private Slider healthSlider; 
        
        private List<SlurButton> _slurButtons = new();
        
        public void InitializeView(List<Slur> slurs)
        {
            foreach (Transform child in Content.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var slur in slurs)
            {
                var button = Instantiate(slurGameObject, Content.transform);
                button.Initialize(slur);
                _slurButtons.Add(button);
            }
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}