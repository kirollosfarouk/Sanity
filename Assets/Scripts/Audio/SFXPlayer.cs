using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance => _instance;

    private static SFXPlayer _instance;
    private AudioSource _source;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
            Destroy(gameObject);

        _source = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}