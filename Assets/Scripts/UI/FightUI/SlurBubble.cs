using Slurs;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SlurBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI slurText;

        public void Initialize(Slur slur)
        {
            slurText.text = slur.ToString();
            slurText.color = CategoryColor.SlurColors[slur.Category[0]];
        }
    }
}