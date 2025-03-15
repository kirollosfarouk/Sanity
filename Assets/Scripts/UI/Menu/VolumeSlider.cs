using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public AudioMixer Mixer;
    public string PropertyName;
    private Slider _slider;

    public void Start()
    {
        _slider = GetComponent<Slider>();
        if (Mixer.GetFloat(PropertyName, out var value))
        {
            _slider.value = Mathf.Pow(10, value / 10);
        }
    }

    public void Update()
    {
        Mixer.SetFloat(PropertyName, 10 * Mathf.Log10(_slider.value));
    }
}