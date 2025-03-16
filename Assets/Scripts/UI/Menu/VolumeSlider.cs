using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public AudioMixer Mixer;
    public string PropertyName;
    private Slider _slider;

    public void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat(PropertyName, 0f);
        Initialize(_slider.value);
        _slider.onValueChanged.AddListener(SetVolume);
    }

    public void OnEnable()
    {
        _slider.value = PlayerPrefs.GetFloat(PropertyName, 0f);
    }

    private async void Initialize(float value)
    {
        await Task.Yield();
        SetVolume(value);
    }

    private void SetVolume(float value)
    {
        Mixer.SetFloat(PropertyName, value);
        PlayerPrefs.SetFloat(PropertyName, value);
        PlayerPrefs.Save();
    }
}