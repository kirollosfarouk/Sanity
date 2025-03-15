using Slurs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SlurButton : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _text;
        
        private Slurs.Slurs slur;
        
        public void Initialize(Slurs.Slurs intialSlurs)
        {
            slur = intialSlurs;
            _text.text = slur.SlurText;
            _text.color = CategoryColor.SlurColors[slur.Category[0]];
            _button.onClick.AddListener(Call);
        }

        private void Call()
        {
            throw new System.NotImplementedException();
        }
    }
}