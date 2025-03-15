using UnityEngine;

[CreateAssetMenu]
public class MusicDefinition : ScriptableObject
{
    public AudioClip Clip;
    public double LoopStartTime;
    public double LoopEndTime;

    public int LoopStartSamples => (int)(LoopStartTime * Clip.frequency);
    public int LoopEndSamples => (int)(LoopEndTime * Clip.frequency);
    public int LoopLengthSamples => LoopEndSamples - LoopStartSamples;
}