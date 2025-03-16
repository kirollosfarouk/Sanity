using Fight;
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

        public AudioClip AudioClip;

        private Slur _slur;

        public void Initialize(Slur intialSlur)
        {
            _slur = intialSlur;
            _text.text = _slur.SlurText;
            _text.color = CategoryColor.SlurColors[_slur.Category[0]];
            _button.onClick.AddListener(Call);
        }

        private void Call()
        {
            FightManager.Instance.WordSelected(_slur);
            SFXPlayer.Instance.PlaySFX(AudioClip);
        }
    }
}