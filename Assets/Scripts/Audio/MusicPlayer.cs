using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance => _instance;

    private static MusicPlayer _instance;
    private MusicDefinition _currentDefinition;
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

    public void StartMusic(MusicDefinition definition)
    {
        if (definition == null || definition == _currentDefinition)
            return;
        _currentDefinition = definition;
        _source.clip = definition.Clip;
        _source.Play();
    }

    public void Update()
    {
        if (_currentDefinition != null && _source.timeSamples >= _currentDefinition.LoopEndSamples)
        {
            Debug.Log($"Looping {_currentDefinition.name}");
            _source.timeSamples -= _currentDefinition.LoopLengthSamples;
        }
    }
}