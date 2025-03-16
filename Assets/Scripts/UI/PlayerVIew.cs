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
        //private GameObject slur
        private void Start()
        {
          //  InitializeSlurs();
        }

        private void InitializeSlurs()
        {
            SlursGenerator.LoadSlurs();
            
            foreach (var slur in SlursGenerator.UnlockedSlurs)  
            {
               var button = Instantiate(slurGameObject,Content.transform);
               button.Initialize(slur);
               _slurButtons.Add(button);
            }            
        }

        public void InitalizeView(List<Slur> slurs)
        {
            foreach (var slur in SlursGenerator.UnlockedSlurs)  
            {
                var button = Instantiate(slurGameObject,Content.transform);
                button.Initialize(slur);
                _slurButtons.Add(button);
            }         
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthSlider.value = currentHealth/maxHealth;
        }
    }
}