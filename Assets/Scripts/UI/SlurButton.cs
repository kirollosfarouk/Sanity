﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SlurButton : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _text;
        
        private Slurs slur;
        
        public void Initialize(Slurs intialSlurs)
        {
            slur = intialSlurs;
            _text.text = slur.SlurText;
        }
    }
}