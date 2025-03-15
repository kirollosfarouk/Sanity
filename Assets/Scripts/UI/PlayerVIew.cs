using Slurs;
using UnityEngine;

namespace UI
{
    public class PlayerVIew : MonoBehaviour
    {
        [SerializeField] private GameObject Content;
        [SerializeField] private SlurButton slurGameObject;
        //private GameObject slur
        private void Start()
        {
            InitializeSlurs();
        }

        private void InitializeSlurs()
        {
            SlursGenerator.LoadSlurs();
            
            foreach (var slur in SlursGenerator.UnlockedSlurs)  
            {
                Instantiate(slurGameObject,Content.transform).Initialize(slur);
            }            
        }
    }
}